using System;
using Microsoft.Azure.WebJobs.Description;

namespace AzureFunctions.Extensions.NotificationHubs.Output
{
    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter)]
    [Binding]
    public class NotificationHubsAttribute : Attribute
    {
        [AppSetting(Default = "DefaultFullSharedAccessSignature")]
        public string Connection { get; set; } = default!;

        [AppSetting(Default = "HubsName")] 
        public string HubsName { get; set; } = default!;
    }
}
