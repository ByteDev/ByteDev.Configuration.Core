using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace ByteDev.Configuration.Core
{
    public class JsonFileConfigurationFactory
    {
        private readonly string _environment;

        public JsonFileConfigurationFactory() : this(null)
        {
        }

        public JsonFileConfigurationFactory(string environment)
        {
            _environment = environment?.ToLower();
        }

        public IConfigurationRoot Create()
        {
            var assembly = typeof(JsonFileConfigurationFactory).GetTypeInfo().Assembly;
            var baseLocation = Directory.GetParent(new Uri(assembly.CodeBase).LocalPath).FullName;

            var builder = new ConfigurationBuilder()
                .SetBasePath(baseLocation)
                .AddAppSettingsJsonFile();

            if (!string.IsNullOrEmpty(_environment))
            {
                builder.AddAppSettingsJsonFile(new ConfigurationFileOptions { Environment = _environment });
            }

            return builder.Build();
        }
    }
}