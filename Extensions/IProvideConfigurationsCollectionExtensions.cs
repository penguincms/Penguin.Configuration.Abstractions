using Penguin.Configuration.Abstractions.Interfaces;
using System;

namespace Penguin.Configuration.Abstractions.Extensions
{
    public static class IProvideConfigurationsCollectionExtensions
    {
        /// <summary>
        /// Searches the included providers for a writable configuration, and saves the value in the first writable provider
        /// </summary>
        /// <param name="collection">The configuration provider collection</param>
        /// <param name="Name">The configuration name to update</param>
        /// <param name="Value">The new value</param>
        /// <returns>True if a writable provider was found to persist the value</returns>
        public static bool SetConfiguration(this IProvideConfigurationsCollection collection, string Name, string Value)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (Name is null)
            {
                throw new ArgumentNullException(nameof(Name));
            }

            bool toReturn = false;

            foreach (IProvideConfigurations pc in collection.Providers)
            {
                if (pc.CanWrite && pc.SetConfiguration(Name, Value))
                {
                    toReturn = true;
                }
            }

            return toReturn;
        }
    }
}