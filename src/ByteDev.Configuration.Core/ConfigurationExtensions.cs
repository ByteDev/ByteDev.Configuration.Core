using Microsoft.Extensions.Configuration;

namespace ByteDev.Configuration.Core
{
    /// <summary>
    /// Extension methods for <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" />.
    /// </summary>
    public static class ConfigurationExtensions
    {
        public static TValue GetApplicationSettingsValue<TValue>(this IConfiguration source, string key)
        {
            return source.GetValue<TValue>(ApplicationSettings.GetKey(key));
        }

        public static TSettings GetApplicationSettings<TSettings>(this IConfiguration source)
        {
            return source.GetSectionSettings<TSettings>(ApplicationSettings.SectionName);
        }

        public static TSettings GetSectionSettings<TSettings>(this IConfiguration source, string section)
        {
            return source
                .GetSection(section)
                .Get<TSettings>();
        }
    }
}