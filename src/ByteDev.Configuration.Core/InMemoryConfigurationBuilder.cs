using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ByteDev.Configuration.Core
{
    /// <summary>
    /// Represents a builder of in memory configurations.
    /// </summary>
    public class InMemoryConfigurationBuilder
    {
        private readonly IList<KeyValuePair<string, string>> _keyValues = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Add an application setting (setting in the "ApplicationSetting" section).
        /// </summary>
        /// <param name="key">Setting key.</param>
        /// <param name="value">Setting value.</param>
        /// <returns>Current builder instance.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="key" /> was null or empty.</exception>
        public InMemoryConfigurationBuilder WithApplicationSetting(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Application setting key was null or empty.", nameof(key));

            WithSetting(ApplicationSettings.GetKey(key), value?.ToString());
            return this;
        }

        /// <summary>
        /// Add a setting.
        /// </summary>
        /// <param name="key">Setting key.</param>
        /// <param name="value">Setting value.</param>
        /// <returns>Current builder instance.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="key" /> was null or empty.</exception>
        public InMemoryConfigurationBuilder WithSetting(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Setting key was null or empty.", nameof(key));

            _keyValues.Add(new KeyValuePair<string, string>(key, value?.ToString()));
            return this;
        }

        /// <summary>
        /// Clears all settings.
        /// </summary>
        /// <returns>Current builder instance.</returns>
        public InMemoryConfigurationBuilder Clear()
        {
            _keyValues.Clear();
            return this;
        }

        /// <summary>
        /// Builds a configuration based on the supplied settings.
        /// </summary>
        /// <returns>Current builder instance.</returns>
        public IConfigurationRoot Build()
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(_keyValues)
                .Build();
        }
    }
}