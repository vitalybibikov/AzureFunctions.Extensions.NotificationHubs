using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureFunctions.Extensions.NotificationHubs.Output;
using Microsoft.Azure.WebJobs;

namespace AzureFunctions.Extensions.NotificationHubs.Interfaces
{
    public interface IAsyncNotificationsHubCollector : IAsyncCollector<HubsMessage>
    {
        Task AddRangeAsync(IEnumerable<HubsMessage> items);

        Task AddAsync(params HubsMessage[] items);
    }
}
