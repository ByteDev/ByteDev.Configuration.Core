using System;
using Microsoft.Extensions.Configuration;

namespace ByteDev.Configuration.Core
{
    /// <summary>
    /// Extension methods for <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.
    /// </summary>
    public static class ConfigurationBuilderExtensions
    {
        /// <summary>
        /// Add settings from a appsettings.json file.
        /// </summary>
        /// <param name="source">Configuration builder to add the appsettings JSON file to.</param>
        /// <returns>Same builder instance.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static IConfigurationBuilder AddAppSettingsJsonFile(this IConfigurationBuilder source)
        {
            return AddAppSettingsJsonFile(source, new ConfigurationFileOptions());
        }

        /// <summary>
        /// Add settings from a appsettings.[environment].json file. Where [environment] is the 
        /// provided options host environment.
        /// </summary>
        /// <param name="source">Configuration builder to add the appsettings JSON file to.</param>
        /// <param name="options">Options for the settings file.</param>
        /// <returns>Same builder instance.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="options" /> is null.</exception>
        public static IConfigurationBuilder AddAppSettingsJsonFile(this IConfigurationBuilder source, ConfigurationFileOptions options)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var fileName = CreateAppSettingsFileName(options.Environment);

            return source.AddJsonFile(fileName, options.IsOptional, options.ReloadOnChange);
        }

        private static string CreateAppSettingsFileName(string environment)
        {
            return string.IsNullOrEmpty(environment) ? "appsettings.json" : $"appsettings.{environment}.json";
        }
    }
}