using AzureFunctions.Extensions.NotificationHubs.Output;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;

namespace AzureFunctions.Extensions.NotificationHubs.Binding
{
    [Extension("NotificationHubsExtensionConfigProvider")]
    public class NotificationHubsExtensionConfigProvider : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<NotificationHubsAttribute>()
                .BindToCollector<HubsMessage>(attr => new NotificationHubsOutputAsyncCollector(attr));
        }
    }
}
