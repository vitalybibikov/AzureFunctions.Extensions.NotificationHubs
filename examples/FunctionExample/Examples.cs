using AzureFunctions.Extensions.NotificationHubs.Enum;
using AzureFunctions.Extensions.NotificationHubs.Interfaces;
using AzureFunctions.Extensions.NotificationHubs.Output;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.NotificationHubs;
using System.Collections.Generic;

namespace FunctionExample
{
    public class Examples
    {
        [FunctionName("SendConfigured")]
        public void SendConfigure(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "messages/configure")] HttpRequest req,
            [NotificationHubs(Connection = "DefaultFullSharedAccessSignature", HubsName = "HubsName")] out HubsMessage output)
        {
            output = new HubsMessage("Notification Hub test message", Platform.Android);
        }

        [FunctionName("SendOne")]
        public void SendOne(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "messages")] HttpRequest req,
            [NotificationHubs] out HubsMessage output)
        {
            output = new HubsMessage("Notification Hub test message", Platform.Android);
        }

        [FunctionName("SendOneWithTags")]
        public void SendWithTags(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "messages/tags")] HttpRequest req,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            var tags = new List<string> {  "registered", "new" };
            output.AddAsync(new HubsMessage("Notification Hub test message", Platform.Android, "registered", "new"));
            output.AddAsync(new HubsMessage("Notification Hub test message", Platform.Android, tags));
        }

        [FunctionName("SendMany")]
        public void SendMany(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "messages/many")] HttpRequest req,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            var message1 = new HubsMessage("Notification Hub test message 1", Platform.Android);
            var message2 = new HubsMessage("Notification Hub test message 3", Platform.Apple);

            var payload = @"{""data"":{""message"":""Notification Hub test notification""}}";
            var notification = new HubsMessage(new AppleNotification(payload));

            output.AddAsync(message1);
            output.AddAsync(message2);
            output.AddAsync(notification);
        }

        [FunctionName("SendAndroid")]
        public void SendAndroid(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "message/android")] HttpRequest req,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            var message = new HubsMessage("Notification Hub test message", Platform.Android);
            output.AddAsync(message);
        }

        [FunctionName("SendApple")]
        public void SendApple(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "message/apple")] HttpRequest req,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            var message = new HubsMessage("Notification Hub test message", Platform.Apple);
            output.AddAsync(message);
        }

        [FunctionName("SendAndroidNotification")]
        public void SendAndroidNotification(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "notification/android")] HttpRequest req,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            var payload = @"{""data"":{""message"":""Notification Hub test notification""}}";
            var notification = new HubsMessage(new FcmNotification(payload));
            output.AddAsync(notification);
        }

        [FunctionName("SendAppleNotification")]
        public void SendAppleNotification(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "notification/apple")] HttpRequest req,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            var payload = @"{""aps"":{""alert"":""Notification Hub test notification""}}";
            var notification = new HubsMessage(new AppleNotification(payload));
            output.AddAsync(notification);
        }


        [FunctionName("SendRest")]
        public void SendRest(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "messages/rest")] HttpRequest req,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            var payload = @"%Replace with a payload that is specific to a platform%";
            var dictionary = new Dictionary<string, string>();

            var notification1 = new HubsMessage(new AppleNotification(payload));
            var notification2 = new HubsMessage(new AdmNotification(payload));
            var notification3 = new HubsMessage(new FcmNotification(payload));
            var notification4 = new HubsMessage(new WindowsNotification(payload));
            var notification5 = new HubsMessage(new TemplateNotification(dictionary));
            var notification6 = new HubsMessage(new BaiduNotification(payload));

            output.AddAsync(notification1);
            output.AddAsync(notification2);
            output.AddAsync(notification3);
            output.AddAsync(notification4);
            output.AddAsync(notification5);
            output.AddAsync(notification6);
        }
    }
}
