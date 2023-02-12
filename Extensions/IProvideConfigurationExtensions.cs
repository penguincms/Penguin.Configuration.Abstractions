using Loxifi;
using Penguin.Configuration.Abstractions.Interfaces;
using Penguin.Extensions.String;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Penguin.Configuration.Abstractions.Extensions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class IProvideConfigurationExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Recursively searches for a connection string by name. Allows for linking connection strings to eachother by name
        /// </summary>
        /// <param name="provider">The provider to use</param>
        /// <param name="toTest">The name (or connection string) to return</param>
        /// <returns>The furthest resolvable value representing the connection string</returns>
        public static string FindConnectionString(this IProvideConfigurations provider, string toTest = "DefaultConnectionString")
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            string ConnectionString = provider.GetConnectionString(toTest) != null
                ? provider.GetConnectionString(toTest)
                : !string.IsNullOrWhiteSpace(provider.GetConfiguration(toTest))
                    ? provider.FindConnectionString(provider.GetConfiguration(toTest))
                    : toTest;
            if (ConnectionString is null)
            {
                throw new Exception("Can not test for null connection string. How did we get here?");
            }

            if (ConnectionString.StartsWith("name=", StringComparison.OrdinalIgnoreCase))
            {
                ConnectionString = ConnectionString.Replace("name=", "");
                ConnectionString = provider.FindConnectionString(ConnectionString);
            }

            return ConnectionString;
        }

        /// <summary>
        /// Gets a configuration value as a bool
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="Name">the name of the configuration value to get</param>
        /// <returns>The configuration value, or false if null</returns>
        public static bool GetBool(this IProvideConfigurations provider, string Name)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            string toReturn = provider.GetConfiguration(Name);
            return toReturn != null && bool.Parse(toReturn);
        }

        /// <summary>
        /// Returns a string value from an IProvideConfigurations as a Dictionary, assuming its delimited in Key=Value; notation
        /// </summary>
        /// <param name="provider">The provider to use as a source</param>
        /// <param name="key">The Key to search for</param>
        /// <returns>A dictionary representing the key value pairs found in the configuration value</returns>
        public static Dictionary<string, string> GetDictionary(this IProvideConfigurations provider, string key)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            string v = provider.GetConfiguration(key);

            return v?.ToDictionary();
        }

        /// <summary>
        /// Gets a configuration as an int
        /// </summary>
        /// <param name="provider">The provider to use</param>
        /// <param name="Name">The name of the configuration to get</param>
        /// <returns>the int representation (or 0 if null)</returns>
        public static int GetInt(this IProvideConfigurations provider, string Name)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            string toReturn = provider.GetConfiguration(Name);
            return toReturn == null ? 0 : int.Parse(toReturn, NumberStyles.Integer, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Checks connection strings first, then configurations
        /// </summary>
        /// <param name="provider">The IConfigurationProvider</param>
        /// <param name="Name">The name of the connection string/configuration</param>
        /// <returns>The value, if any is found, or null</returns>
        public static string ConnectionStringOrConfiguration(this IProvideConfigurations provider, string Name)
        {
            return provider is null
                ? throw new ArgumentNullException(nameof(provider))
                : provider.GetConnectionString(Name) ?? provider.GetConfiguration(Name);
        }

        /// <summary>
        /// Checks for the configuration "Debug" and returns its value
        /// </summary>
        /// <param name="provider">The provider to use</param>
        /// <returns>The value of "Debug"</returns>
        public static bool IsDebug(this IProvideConfigurations provider)
        {
            return provider.GetBool("Debug");
        }
    }
}