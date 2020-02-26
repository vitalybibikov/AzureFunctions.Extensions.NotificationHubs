using System;
using AzureFunctions.Extensions.NotificationHubs.Binding;
using Microsoft.Azure.WebJobs;

namespace AzureFunctions.Extensions.NotificationHubs.Extensions
{
    public static class WebJobsBuilderExtensions
    {
        public static IWebJobsBuilder AddNotificationHubs(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.AddExtension<NotificationHubsExtensionConfigProvider>();
            return builder;
        }
    }
}
