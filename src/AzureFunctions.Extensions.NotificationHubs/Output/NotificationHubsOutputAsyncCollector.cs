using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AzureFunctions.Extensions.NotificationHubs.Hubs;
using AzureFunctions.Extensions.NotificationHubs.Interfaces;
using Microsoft.Azure.NotificationHubs;

namespace AzureFunctions.Extensions.NotificationHubs.Output
{
    public class NotificationHubsOutputAsyncCollector : IAsyncNotificationsHubCollector
    {
        private readonly NotificationHubClient client;

        private readonly List<HubsMessage> notificationsPool = new List<HubsMessage>();

        public NotificationHubsOutputAsyncCollector(
            NotificationHubsAttribute attribute)
        {
            if (attribute == null)
            {
                 throw new ArgumentNullException(nameof(attribute));
            }

            if (attribute.Connection == null)
            {
                throw new ArgumentNullException(nameof(attribute.Connection));
            }

            if (attribute.HubsName == null)
            {
                throw new ArgumentNullException(nameof(attribute.Connection));
            }

            client = NotificationsManager.GetClient(attribute.Connection, attribute.HubsName);
        }

        public Task AddAsync(HubsMessage item, CancellationToken token = default)
        {
            if (item?.Notification != null)
            {
                notificationsPool.Add(item);
            }

            return Task.CompletedTask;
        }

        public Task AddRangeAsync(IEnumerable<HubsMessage> items)
        {
            if (items != null)
            {
                notificationsPool.AddRange(items);
            }

            return Task.CompletedTask;
        }

        public Task AddAsync(params HubsMessage[] items)
        {
            AddRangeAsync(items);
            return Task.CompletedTask;
        }

        public async Task FlushAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in notificationsPool)
            {
                if (item.Tags.Any())
                {
                    await client.SendNotificationAsync(item.Notification, item.Tags, cancellationToken);
                }
                else
                {
                    await client.SendNotificationAsync(item.Notification, cancellationToken);
                }
            }

            notificationsPool.Clear();
        }
    }
}
