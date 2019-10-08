using Penguin.Configuration.Abstractions.Interfaces;
using Penguin.Extensions.Strings;
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

            string ConnectionString;

            if (provider.GetConnectionString(toTest) != null)
            {
                ConnectionString = provider.GetConnectionString(toTest);
            }
            else if (!string.IsNullOrWhiteSpace(provider.GetConfiguration(toTest)))
            {
                ConnectionString = provider.FindConnectionString(provider.GetConfiguration(toTest));
            }
            else
            {
                ConnectionString = toTest;
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

            if (v is null)
            {
                return null;
            }
            else
            {
                return v.ToDictionary();
            }
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
        /// Checks for the configuration "Debug" and returns its value
        /// </summary>
        /// <param name="provider">The provider to use</param>
        /// <returns>The value of "Debug"</returns>
        public static bool IsDebug(this IProvideConfigurations provider) => provider.GetBool("Debug");
    }
}