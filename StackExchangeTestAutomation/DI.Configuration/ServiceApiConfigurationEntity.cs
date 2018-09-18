namespace DI.Configuration
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("TestConfiguration")]
    public class ServiceApiConfigurationEntity
    {
        public string EnvironmentUrl { get; set; }
    }
}
