using System;
using AzureFunctions.Extensions.NotificationHubs.Enum;
using Microsoft.Azure.NotificationHubs;

namespace AzureFunctions.Extensions.NotificationHubs.Extensions
{
    internal static class StringExtensions
    {
        public static Notification ToNotification(this string message, Platform platform)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            switch (platform)
            {
                case Platform.Apple:
                    var alertTemplate = $"{{'aps':{{'alert':'{message}'}}}}";
                    return new AppleNotification(alertTemplate);

                case Platform.Android:
                    var messageTemplate = $"{{'data':{{'message':'{message}'}}}}";
                    return new FcmNotification(messageTemplate);

                default:
                    throw new NotSupportedException($"{nameof(platform)} is not supported;");
            }
        }
    }
}
