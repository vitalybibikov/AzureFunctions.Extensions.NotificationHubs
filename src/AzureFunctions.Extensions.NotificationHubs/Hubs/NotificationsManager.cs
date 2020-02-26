using System;
using System.Collections.Generic;
using Microsoft.Azure.NotificationHubs;

namespace AzureFunctions.Extensions.NotificationHubs.Hubs
{
    internal static class NotificationsManager
    {
        private static readonly Dictionary<(string, string), NotificationHubClient> NotificationsClient
            = new Dictionary<(string, string), NotificationHubClient>();

        public static NotificationHubClient GetClient(string connection, string name)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!NotificationsClient.TryGetValue((connection, name), out var value))
            {
                value = NotificationHubClient.CreateClientFromConnectionString(connection, name);
                NotificationsClient.Add((connection, name), value);
            }

            return value;
        }
    }
}
