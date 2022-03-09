namespace ByteDev.Configuration.Core
{
    /// <summary>
    /// Represents options for configuration files.
    /// </summary>
    public class ConfigurationFileOptions
    {
        /// <summary>
        /// Any environment the settings file is associated with. For example: "dev", "uat", etc.
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// Indicates if the file is optional.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Indicates if the configuration should be reloaded if the file changes.
        /// </summary>
        public bool ReloadOnChange { get; set; }
    }
}