using System;

namespace ByteDev.Configuration.Core
{
    internal static class ApplicationSettings
    {
        public const string SectionName = "ApplicationSettings";

        public static string GetKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Application settings key was null or empty.", nameof(key));

            return $"{SectionName}:{key}";
        }
    }
}