using System;

namespace Penguin.Configuration.Abstractions.Exceptions
{
    public class MissingConfigurationException : Exception
    {
        public string ConfigurationName { get; set; }

        public MissingConfigurationException(string configurationName, string message) : base(message)
        {
            ConfigurationName = configurationName;
        }

        public MissingConfigurationException(string configurationName, string message, Exception innerException) : base(message, innerException)
        {
            ConfigurationName = configurationName;
        }
    }
}