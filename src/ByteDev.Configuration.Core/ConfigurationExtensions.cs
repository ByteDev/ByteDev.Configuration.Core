using Microsoft.Extensions.Configuration;

namespace ByteDev.Configuration.Core
{
    public static class ConfigurationExtensions
    {
        public static TSettings GetApplicationSettings<TSettings>(this IConfiguration source)
        {
            return source.GetSectionSettings<TSettings>("ApplicationSettings");
        }

        public static TSettings GetSectionSettings<TSettings>(this IConfiguration source, string section)
        {
            return source
                .GetSection(section)
                .Get<TSettings>();
        }
    }
}