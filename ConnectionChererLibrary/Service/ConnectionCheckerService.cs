using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ConnectionCheckerLibrary.DataBase.Models;
using ConnectionCheckerLibrary.DataBase.Repository;
using ConnectionCheckerLibrary.Service.Models;

namespace ConnectionCheckerLibrary.Service
{
    /// <summary>
    /// The ConnectionCheckerService interface.
    /// </summary>
    public interface IConnectionCheckerService
    {
        /// <summary>
        /// Gets the connection status states.
        /// </summary>
        ConcurrentDictionary<Connection, ConnectionStatus> ConnectionStatusStates { get; }

        /// <summary>
        /// The update status of connection event.
        /// </summary>
        event ConnectionCheckerService.UpdateStatusOfConnection UpdateStatusOfConnectionEvent;

        /// <summary>
        /// The start connection checking.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        void StartConnectionCheck(CancellationToken cancellationToken);

        /// <summary>
        /// The start connection check for the specific connection
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task StartConnectionCheck(Connection connection, CancellationToken token = default);

        /// <summary>
        /// The remove connection.
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        void RemoveConnection(Connection connection);
    }

    /// <summary>
    /// The connection checker service.
    /// </summary>
    public class ConnectionCheckerService : IConnectionCheckerService
    {
        /// <summary>
        /// The update status of connection.
        /// </summary>
        /// <param name="eventsArgs">
        /// The events args.
        /// </param>
        public delegate void UpdateStatusOfConnection(UpdateStatusOfConnectionEventsArgs eventsArgs);

        /// <summary>
        /// The update status of connection event.
        /// </summary>
        public event UpdateStatusOfConnection UpdateStatusOfConnectionEvent;

        /// <summary>
        /// The http client.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The _connection repository.
        /// </summary>
        private readonly IBaseRepository<Connection> _connectionRepository;

        /// <summary>
        /// The connection repository.
        /// </summary>
        public IBaseRepository<Connection> ConnectionRepository => _connectionRepository;

        /// <summary>
        /// The _connection status states.
        /// </summary>
        private ConcurrentDictionary<Connection, ConnectionStatus> _connectionStatusStates;

        /// <summary>
        /// The connection status states.
        /// </summary>
        public ConcurrentDictionary<Connection, ConnectionStatus> ConnectionStatusStates => _connectionStatusStates;


        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionCheckerService"/> class.
        /// </summary>
        /// <param name="connectionRepository">
        /// The connection repository.
        /// </param>
        public ConnectionCheckerService(IBaseRepository<Connection> connectionRepository)
        {
            _httpClient = new HttpClient();
            _connectionRepository = connectionRepository;
            _connectionStatusStates = new ConcurrentDictionary<Connection, ConnectionStatus>();
        }

        /// <summary>
        /// The start connection checking.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        public void StartConnectionCheck(CancellationToken token = default)
        {
            _connectionRepository.GetAll().ToList().ForEach(connection =>
            {
                StartConnectionCheck(connection, token);
            });
        }

        /// <summary>
        /// The start connection check.
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task StartConnectionCheck(Connection connection, CancellationToken token = default)
        {
            ConnectionStatus connectionStatus = _connectionStatusStates.GetOrAdd(connection, new ConnectionStatus());

            connectionStatus.Connection = connection;
            connectionStatus.ConnectionStatusState = ConnectionStatusState.Started;

            return Task.Run(() => StartConnectionCheckAsync(connection, token), token);
        }

        /// <summary>
        /// The start connection check async.
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        public async void StartConnectionCheckAsync(Connection connection, CancellationToken token = default)
        {
            while (!token.IsCancellationRequested || connection != null)
            {
                try
                {
                    ConnectionStatus connectionStatus = _connectionStatusStates.GetOrAdd(connection, new ConnectionStatus());

                    if (connectionStatus.ConnectionStatusState == ConnectionStatusState.Deleted)
                    {
                        _connectionStatusStates.TryRemove(connection, out connectionStatus);
                        return;
                    }

                    if (connection.IsOn)
                    {
                        connectionStatus.ConnectionStatusState = ConnectionStatusState.Started;

                        await Task.Run(() => CheckConnectionStatusAsync(connection, token), token);
                    }
                    else
                    {
                        connectionStatus.ConnectionStatusState = ConnectionStatusState.Off;
                    }

                    await Task.Delay(TimeSpan.FromSeconds(connection.CheckDelay), token);
                }
                catch (TaskCanceledException)
                {
                }
            }
        }

        /// <summary>
        /// The remove connection.
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        public void RemoveConnection(Connection connection)
        {
            ConnectionStatus connectionStatus;
            bool valueExist = _connectionStatusStates.TryGetValue(connection, out connectionStatus);

            if (valueExist)
            {
                connectionStatus.ConnectionStatusState = ConnectionStatusState.Deleted;
            }
        }

        /// <summary>
        /// The check connection status async.
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        public async void CheckConnectionStatusAsync(Connection connection, CancellationToken token = default)
        {
            string result = null;

            try
            {
                result = await _httpClient.GetStringAsync(connection.URL);
            }
            catch (Exception e)
            {
            }

            ConnectionStatus connectionStatus = _connectionStatusStates.GetOrAdd(connection, new ConnectionStatus());

            if (connectionStatus.ConnectionStatusState == ConnectionStatusState.Off || connectionStatus.ConnectionStatusState == ConnectionStatusState.Deleted)
            {
                return;
            }

            connectionStatus.Connection = connection;
            connectionStatus.ConnectionCheckDateTime = DateTime.Now;

            if (result != null)
            {
                connectionStatus.ConnectionStatusState = ConnectionStatusState.Available;
                connectionStatus.SuccessConnectionsCount = connectionStatus.SuccessConnectionsCount + 1;
            }
            else
            {
                connectionStatus.ConnectionStatusState = ConnectionStatusState.Unavailable;
                connectionStatus.BadConnectionsCount = connectionStatus.BadConnectionsCount + 1;
            }

            _connectionStatusStates.AddOrUpdate(connection, connectionStatus,
                (connection1, status) => connectionStatus);

            OnUpdatedStatusOfConnectionEvent(new UpdateStatusOfConnectionEventsArgs() { ConnectionStatus = connectionStatus });
        }

        /// <summary>
        /// The on updated status of connection event.
        /// </summary>
        /// <param name="eventsArgs">
        /// The events args.
        /// </param>
        protected virtual void OnUpdatedStatusOfConnectionEvent(UpdateStatusOfConnectionEventsArgs eventsArgs)
        {
            UpdateStatusOfConnectionEvent?.Invoke(eventsArgs);
        }
    }
}
