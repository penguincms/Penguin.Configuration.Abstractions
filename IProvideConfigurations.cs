using System.Collections.Generic;

namespace Penguin.Configuration.Abstractions
{
    /// <summary>
    /// An interface for any object that provides configuration information to the application
    /// </summary>
    public interface IProvideConfigurations
    {
        /// <summary>
        /// Returns a dictionary of all found configurations
        /// </summary>
        Dictionary<string, string> AllConfigurations { get; }

        /// <summary>
        /// Returns a list of all connection string values
        /// </summary>
        Dictionary<string, string> AllConnectionStrings { get; }

        /// <summary>
        /// Returns a single configuration value (or null)
        /// </summary>
        /// <param name="Key">The configuration key</param>
        /// <returns>The configuration value</returns>
        string GetConfiguration(string Key);

        /// <summary>
        /// Gets a connection string by name
        /// </summary>
        /// <param name="Name">The name of the connection string</param>
        /// <returns>The connection string value</returns>
        string GetConnectionString(string Name);
    }
}