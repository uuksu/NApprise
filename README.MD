# NApprise

[![.CI](https://github.com/uuksu/NApprise/actions/workflows/CI.yml/badge.svg)](https://github.com/uuksu/NApprise/actions/workflows/CI.yml)
[![NuGet](https://img.shields.io/nuget/dt/napprise.svg)](https://www.nuget.org/packages/napprise) 
[![NuGet](https://img.shields.io/nuget/vpre/napprise.svg)](https://www.nuget.org/packages/napprise)

Simple .NET Client for great [Apprise](https://github.com/caronc/apprise) powered by OpenAPI/NSwag.

Uses [Apprise API](https://github.com/caronc/apprise-api) as a target service.

NApprise targets .NET Standard 2.0.

## Installing NApprise

You should install NApprise with NuGet:

    Install-Package NApprise

Or by using the .NET CLI:

    dotnet add package NApprise

## Requirements

You should have a instance of Apprise API running. Refer [installation guide](https://github.com/caronc/apprise-api#installation) found in the original repository.

## Usage

### Stateless

In staless mode nothing is stored on the server side. This can be also described as a ad hoc.

``` csharp
using NApprise;

var statelessClient = new AppriseStatelessClient("http://localhost:8000", new HttpClient());

// Send stateless notification

await statelessClient.SendNotificationAsync(new StatelessNotificationRequest() {
    Urls = new List<string> { "mailto:///example:mypassword@hotmail.com" },
    Body = "# Hello World, with Markdown",
    Title = "Hello from header",
    Type = NotificationType.Info,
    Format = NotificationFormat.Markdown
});

```

### Persistent

In persistent mode, preconfigured services can be stored, managed and called.

``` csharp

using NApprise;

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
```