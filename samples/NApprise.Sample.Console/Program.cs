// See https://aka.ms/new-console-template for more information
using NApprise;

// Send stateless notification

var statelessClient = new AppriseStatelessClient("http://localhost:8000", new HttpClient());

await statelessClient.SendNotificationAsync(new StatelessNotificationRequest() {
    Urls = new List<string> { "mailto:///example:mypassword@hotmail.com" },
    Body = "# Hello World, with Markdown",
    Title = "Hello from header",
    Type = NotificationType.Info,
    Format = NotificationFormat.Markdown
});

var persistentClient = new ApprisePersistentClient("http://localhost:8000", new HttpClient());

// Add urls for configuration

await persistentClient.AddConfigurationAsync("mykey", new AddConfigurationRequest() {
    Urls = new List<string> { "mailto:///example:mypassword@hotmail.com", "tgram://123456789:abcdefg_hijklmnop/12315544/" },
});

// Or add configuration text

await persistentClient.AddConfigurationAsync("mykey", new AddConfigurationRequest() {
    Config = "me=mailto:///example:mypassword@hotmail.com\nfamily,me=tgram://123456789:abcdefg_hijklmnop/12315544/",
    Format = AddConfigurationRequestFormat.Text
});

// Get configuration text

var configurationResult = await persistentClient.GetConfigurationAsync("mykey");

// Get urls and tags for configuration

var urlsResult = await persistentClient.GetUrlsAsync("mykey", Privacy.HideSecrets, "family");

// Send notification with stored configuration

await persistentClient.SendNotificationAsync("mykey", new PersistentNotificationRequest() {
    Body = "# Hello World, with Markdown",
    Title = "Hello from header",
    Type = NotificationType.Info,
    Format = NotificationFormat.Markdown,
    Tag = "family"
});

// Remove configuration

await persistentClient.RemoveConfigurationAsync("mykey");

Console.ReadKey();