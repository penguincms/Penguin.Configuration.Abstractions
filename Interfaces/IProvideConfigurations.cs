using System.Collections.Generic;

namespace Penguin.Configuration.Abstractions.Interfaces
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
        /// Can this provider persist values?
        /// </summary>
        bool CanWrite { get; }

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

        /// <summary>
        /// Sets a configuration value
        /// </summary>
        /// <param name="Name">The name of the configuration to set</param>
        /// <param name="Value">The value to set it to</param>
        /// <returns>True if succeeded</returns>
        bool SetConfiguration(string Name, string Value);
    }
}