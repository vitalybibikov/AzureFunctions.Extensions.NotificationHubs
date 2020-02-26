using System;
using System.Collections.Generic;
using System.Linq;
using AzureFunctions.Extensions.NotificationHubs.Enum;
using AzureFunctions.Extensions.NotificationHubs.Extensions;
using Microsoft.Azure.NotificationHubs;

namespace AzureFunctions.Extensions.NotificationHubs.Output
{
    public class HubsMessage
    {
        public HubsMessage(Notification notification, params string[] tags)
        {
            Notification = notification ?? throw new ArgumentNullException(nameof(notification));

            if (tags?.Length > 0)
            {
                Tags.AddRange(tags);
            }
        }

        public HubsMessage(Notification notification, IEnumerable<string> tags)
        {
            Notification = notification ?? throw new ArgumentNullException(nameof(notification));

            if (tags?.Count() > 0)
            {
                Tags.AddRange(tags);
            }
        }

        public HubsMessage(string message, Platform platform, params string[] tags)
            : this(message.ToNotification(platform), tags)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }
        }

        public HubsMessage(string message, Platform platform, IEnumerable<string> tags)
            : this(message.ToNotification(platform), tags)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }
        }

        public Notification Notification { get; }

        public List<string> Tags { get; } = new List<string>();
    }
}
