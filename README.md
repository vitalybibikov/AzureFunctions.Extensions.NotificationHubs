# AzureFunctions.Extensions.NotificationHubs

Notification Hubs Output Binding Extension, that supports Azure Functions v3. Allows to send multiple notifications
to different platforms, that are supported by Azure NotificaionHubs service.

The binding allows you to send multiple notificaions via Azure Notification Hubs service like so:

Step 1.
1. Add the nuget *AzureFunctions.Extensions.NotificationHubs*
2. Add to Startup file the following code.  Currently, accepts simple JWK tokens or tokens loaded out of Azure B2C

```
            builder.AddNotificationHubs();
```
Setup settings:

```
    "DefaultFullSharedAccessSignature": "Endpoint= ...",
    "HubsName": "Enter hubs name here..."
```



2. Add it to Azure Function:

```
        [FunctionName("SendOne")]
        public void SendOne(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "messages")] HttpRequest req,
            [NotificationHubs] out HubsMessage output)
        {
            output = new HubsMessage("Notification Hub test message", Platform.Android);
        }
```

3. Different platforms/configurations are possible.


```
        [FunctionName("SendConfigured")]
        public void SendConfigure(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "messages/configure")] HttpRequest req,
            [NotificationHubs(Connection = "DefaultFullSharedAccessSignature", HubsName = "HubsName")] out HubsMessage output)
        {
            output = new HubsMessage("Notification Hub test message", Platform.Android);
        }
```

OR


```
        [FunctionName("SendOneWithTags")]
        public void SendWithTags(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "messages/tags")] HttpRequest req,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            var tags = new List<string> {  "registered", "new" };
            output.AddAsync(new HubsMessage("Notification Hub test message", Platform.Android, "registered", "new"));
            output.AddAsync(new HubsMessage("Notification Hub test message", Platform.Android, tags));
        }
```


OR


```
        [FunctionName("SendAppleNotification")]
        public void SendAppleNotification(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "notification/apple")] HttpRequest req,
            [NotificationHubs] IAsyncCollector<HubsMessage> output)
        {
            var payload = @"{""aps"":{""alert"":""Notification Hub test notification""}}";
            var notification = new HubsMessage(new AppleNotification(payload));
            output.AddAsync(notification);
        }
        
OR


```
        [FunctionName("SendConfigured")]
        public void SendConfigure(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "messages/configure")] HttpRequest req,
            [NotificationHubs(Connection = "DefaultFullSharedAccessSignature", HubsName = "HubsName")] out HubsMessage output)
        {
            output = new HubsMessage("Notification Hub test message", Platform.Android);
        }
        
OR


```
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
```


3.  Enjoy



