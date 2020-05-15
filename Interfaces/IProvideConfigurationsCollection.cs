using System.Collections.Generic;

namespace Penguin.Configuration.Abstractions.Interfaces
{
    public interface IProvideConfigurationsCollection : IProvideConfigurations
    {
        /// <summary>
        /// An enumerable of the underlying providers contained in this collection
        /// </summary>
        IEnumerable<IProvideConfigurations> Providers { get; }
    }
}