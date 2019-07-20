using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConnectionCheckerLibrary.Service.Models;

namespace ConnectionChecker.Helpers
{
    public class ConnectionStatusHelper
    {
        public static string ConnectionStatusStateClass(ConnectionStatusState connectionStatusState)
        {
            switch (connectionStatusState)
            {
                case ConnectionStatusState.Available:
                {
                    return "success";
                }

                case ConnectionStatusState.Unavailable:
                {
                    return "danger";
                }

                case ConnectionStatusState.Started:
                {
                    return "info";
                }

                case ConnectionStatusState.Off:
                {
                    return "secondary";
                }

                default:
                {
                    return "primary";
                }
            }
        }
    }
}