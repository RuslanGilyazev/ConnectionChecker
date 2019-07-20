using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ConnectionCheckerLibrary.DataBase.Models;
using ConnectionCheckerLibrary.DataBase.Repository;
using ConnectionCheckerLibrary.Service.Models;

namespace ConnectionCheckerLibrary.Service
{
    public interface IConnectionCheckerService
    {
        ConcurrentDictionary<Connection, ConnectionStatus> ConnectionStatusStates { get; }

        event ConnectionCheckerService.UpdateStatusOfConnection UpdateStatusOfConnectionEvent;

        void StartConnectionChecking(CancellationToken cancellationToken);

        Task StartConnectionCheck(Connection connection, CancellationToken token = default);

        void StartConnectionCheckAsync(Connection connection, CancellationToken token = default);

        void RemoveConnection(Connection connection);
    }

    public class ConnectionCheckerService : IConnectionCheckerService
    {
        public delegate void UpdateStatusOfConnection(UpdateStatusOfConnectionEventsArgs eventsArgs);

        public event UpdateStatusOfConnection UpdateStatusOfConnectionEvent;

        private readonly HttpClient _httpClient;

        private readonly ConnectionRepository _connectionRepository;

        private ConcurrentDictionary<Connection, ConnectionStatus> _connectionStatusStates;

        public ConcurrentDictionary<Connection, ConnectionStatus> ConnectionStatusStates => _connectionStatusStates;

        public ConnectionRepository ConnectionRepository => _connectionRepository;

        private object locker = new object();

        public ConnectionCheckerService(ConnectionRepository connectionRepository)
        {
            _httpClient =  new HttpClient();
            _connectionRepository = connectionRepository;
            _connectionStatusStates = new ConcurrentDictionary<Connection, ConnectionStatus>();
        }

        public void StartConnectionChecking(CancellationToken token = default)
        {
            _connectionRepository.GetAll().ToList().ForEach(connection =>
            {
                StartConnectionCheck(connection, token);
            });
        }

        public Task StartConnectionCheck(Connection connection, CancellationToken token = default)
        {
            ConnectionStatus connectionStatus = _connectionStatusStates.GetOrAdd(connection, new ConnectionStatus());

            connectionStatus.Connection = connection;
            connectionStatus.ConnectionStatusState = ConnectionStatusState.Started;

            return Task.Run(() => StartConnectionCheckAsync(connection, token), token);
        }

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

        public void RemoveConnection(Connection connection)
        {
            ConnectionStatus connectionStatus;
            bool valueExist = _connectionStatusStates.TryGetValue(connection, out connectionStatus);

            if (valueExist)
            {
                connectionStatus.ConnectionStatusState = ConnectionStatusState.Deleted;
            }
        }

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

        protected virtual void OnUpdatedStatusOfConnectionEvent(UpdateStatusOfConnectionEventsArgs eventsArgs)
        {
            UpdateStatusOfConnectionEvent?.Invoke(eventsArgs);
        }
    }
}
