using Microsoft.Extensions.Configuration;

namespace ByteDev.Configuration.Core
{
    /// <summary>
    /// Extension methods for <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" />.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Extracts a section based on specified key and binds it to the specified type.
        /// If the section does not exist then null will be returned.
        /// </summary>
        /// <typeparam name="TSettings">Type to return.</typeparam>
        /// <param name="source">Configuration to perform the operation on.</param>
        /// <param name="key">Key to the section.</param>
        /// <returns>New <typeparamref name="TSettings" /> instance.</returns>
        public static TSettings GetSectionSettings<TSettings>(this IConfiguration source, string key)
        {
            return source
                .GetSection(key)
                .Get<TSettings>();
        }

        /// <summary>
        /// Extracts the "ApplicationSettings" section and binds it to the specified type.
        /// If no "ApplicationSettings" sections exists then null will be returned.
        /// </summary>
        /// <typeparam name="TSettings">Type to bind the extracted section of configuration to.</typeparam>
        /// <param name="source">Configuration to perform the operation on.</param>
        /// <returns>New <typeparamref name="TSettings" /> instance.</returns>
        public static TSettings GetApplicationSettings<TSettings>(this IConfiguration source)
        {
            return source.GetSectionSettings<TSettings>(ApplicationSettings.SectionName);
        }

        /// <summary>
        /// Extracts the value with the specified key from the "ApplicationSettings"
        /// section and converts it to type <typeparamref name="TValue" />.
        /// </summary>
        /// <typeparam name="TValue">Type to convert the extracted value to.</typeparam>
        /// <param name="source">Configuration to perform the operation on.</param>
        /// <param name="key">Key to the value within the "ApplicationSettings" section.</param>
        /// <returns>Value from "ApplicationSettings" section.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="key" /> is null or empty.</exception>
        public static TValue GetApplicationSettingsValue<TValue>(this IConfiguration source, string key)
        {
            if (typeof(TValue).IsArray)
            {
                // Arrays are considered sections in configuration
                return source.GetSectionSettings<TValue>(ApplicationSettings.GetKey(key));
            }
            
            return source.GetValue<TValue>(ApplicationSettings.GetKey(key));
        }
    }
}