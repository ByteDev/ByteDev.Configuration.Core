[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Configuration.Core?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Configuration-Core/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Configuration.Core.svg)](https://www.nuget.org/packages/ByteDev.Configuration.Core)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.Configuration.Core/blob/master/LICENSE)

# ByteDev.Configuration.Core

Collection of classes to help when dealing with configuration settings in .NET Core.

## Installation

ByteDev.Configuration.Core is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Configuration.Core`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Configuration.Core/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.Configuration.Core/blob/master/docs/RELEASE-NOTES.md).

## Usage

Functionality includes:
- `IConfigurationBuilder` extensions
- `IConfiguration` extensions
- `InMemoryConfigurationBuilder` class

---

### IConfigurationBuilder extension methods

Build a configuration from JSON settings files:

```csharp
// Add a default settings file (appsettings.json)
var config = new ConfigurationBuilder()
    .AddAppSettingsJsonFile()
    .Build();
```

```csharp
// Add a UAT settings file (appsettings.uat.json)
var config = new ConfigurationBuilder()
    .AddAppSettingsJsonFile(new ConfigurationFileOptions
    {
        Environment = "uat",
        IsOptional = false,
        ReloadOnChange = true
    })
    .Build();
```

---

### IConfiguration extension methods

Retrieve settings from the configuration:

```csharp
// Get "MySettings" section bound to a type
MySettings settings = config.GetSectionSettings<MySettings>("MySettings");

// Get "ApplicationSettings" section bound to a type
MyAppSettings settings = config.GetApplicationSettings<MyAppSettings>();

// Get a particular "ApplicationSettings" section value
string name = config.GetApplicationSettingsValue<string>("Name");
```

---

### InMemoryConfigurationBuilder

The `InMemoryConfigurationBuilder` class allows a configuration to be built quickly in memory (rather than from a settings JSON file).

```csharp
var builder = new InMemoryConfigurationBuilder();

// Add a setting
builder.WithSetting("KeyVaultUri", "https://mykeyvault/")

// Add a setting in the "ApplicationSettings" section
builder.WithApplicationSetting("Name", "John");

// Build the configuration
var config = builder.Build();
```