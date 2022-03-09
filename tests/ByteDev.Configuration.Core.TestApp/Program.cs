using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace ByteDev.Configuration.Core.TestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var jsonConfig = new ConfigurationBuilder()
                .SetBasePath(GetBaseLocation())
                .AddAppSettingsJsonFile()
                .Build();

            // Bind
            var jsonSettings = jsonConfig.GetApplicationSettings<MyApplicationSettings>();

            // Read individual values from configuration
            var s1 = jsonConfig.GetValue<Uri>("KeyVaultUri");
            var s2 = jsonConfig.GetApplicationSettingsValue<string>("SomeString");
            var s3 = jsonConfig.GetApplicationSettingsValue<string>("DoesNotExist");
            
            // ------------

            var inMemConfig = new InMemoryConfigurationBuilder()
                .WithApplicationSetting("SomeString", "Some in memory string")
                .WithApplicationSetting("SomeInt", 100)
                .WithApplicationSetting("SomeUri", new Uri("https://localhost/"))
                .WithApplicationSetting("SomeGuid1", Guid.NewGuid())
                .WithApplicationSetting("SomeGuid2", Guid.NewGuid())
                .WithApplicationSetting("SomeGuid3", Guid.NewGuid())
                .Build();

            // Bind
            var inMemSettings = inMemConfig.GetApplicationSettings<MyApplicationSettings>();

            // Read individual values from configuration
            var result = inMemConfig.GetApplicationSettingsValue<int>("SomeInt");
        }

        private static string GetBaseLocation()
        {
            var assembly = typeof(Program).GetTypeInfo().Assembly;
            return Directory.GetParent(new Uri(assembly.CodeBase).LocalPath).FullName;
        }
    }

    public class MyApplicationSettings
    {
        public string SomeString { get; set; }

        public int SomeInt { get; set; }

        public Uri SomeUri { get; set; }

        public Guid SomeGuid1 { get; set; } // "5fa007f1-551f-4e3f-93dc-707dbe788e1f"

        public Guid SomeGuid2 { get; set; } // "5fa007f1551f4e3f93dc707dbe788e1f"

        public Guid SomeGuid3 { get; set; } // "{5fa007f1551f4e3f93dc-707dbe788e1f}"
    }
}
