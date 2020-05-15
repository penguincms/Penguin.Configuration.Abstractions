using System;

namespace Penguin.Configuration.Abstractions.Exceptions
{
    /// <summary>
    /// An exception thrown when a configuration source is missing a required configuration
    /// </summary>
    public class MissingConfigurationException : Exception
    {
        /// <summary>
        /// The name of the configuration that was requested
        /// </summary>
        public string ConfigurationName { get; protected set; }

        /// <summary>
        /// Constructs a new instance of this exception with the provided configuration name and message
        /// </summary>
        /// <param name="configurationName">The name of the configuration that was requested</param>
        /// <param name="message">The message to return</param>
        public MissingConfigurationException(string configurationName, string message) : base(message)
        {
            this.ConfigurationName = configurationName;
        }

        /// <summary>
        /// Constructs a new instance of this exception with the provided configuration name, message, and inner exception
        /// </summary>
        /// <param name="configurationName">The name of the configuration that was requested</param>
        /// <param name="message">The message to return</param>
        /// <param name="innerException">The exception to wrap</param>
        public MissingConfigurationException(string configurationName, string message, Exception innerException) : base(message, innerException)
        {
            this.ConfigurationName = configurationName;
        }

        internal MissingConfigurationException()
        {
        }

        internal MissingConfigurationException(string message) : base(message)
        {
        }

        internal MissingConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}