using System;
using Microsoft.Extensions.Configuration;

namespace ByteDev.Configuration.Core.TestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var jsonConfig = new JsonFileConfigurationFactory().Create();

            // Read individual values from configuration
            var s1 = jsonConfig.GetValue<Uri>("KeyVaultUri");
            var s2 = jsonConfig.GetValue<string>("ApplicationSettings:SomeString");
            var s3 = jsonConfig.GetValue<string>("ApplicationSettings:DoesNotExist");

            var jsonSettings = jsonConfig.GetApplicationSettings<ApplicationSettings>();
            
            // ------------

            var inMemConfig = new InMemoryConfigurationBuilder()
                .WithApplicationSetting("SomeString", "Some in memory string")
                .WithApplicationSetting("SomeInt", "100")
                .Build();

            var inMemSettings = inMemConfig.GetApplicationSettings<ApplicationSettings>();

            var result = inMemConfig.GetValue(typeof(string), "SomeString");
        }
    }

    public class ApplicationSettings
    {
        public string SomeString { get; set; }

        public int SomeInt { get; set; }

        public Uri SomeUri { get; set; }

        public Guid SomeGuid1 { get; set; } // "5fa007f1-551f-4e3f-93dc-707dbe788e1f"

        public Guid SomeGuid2 { get; set; } // "5fa007f1551f4e3f93dc707dbe788e1f"

        public Guid SomeGuid3 { get; set; } // "{5fa007f1551f4e3f93dc-707dbe788e1f}"
    }
}
