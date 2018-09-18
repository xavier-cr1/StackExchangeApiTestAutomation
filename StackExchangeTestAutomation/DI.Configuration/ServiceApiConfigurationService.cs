namespace DI.Configuration
{
    using System.IO;
    using System.Xml.Serialization;

    public static class ServiceApiConfigurationService
    {
        private const string XmlFileConfigurationPath = @"\..\StackExchangeTestAutomation\DI.Configuration\ConfigurationFiles\TestConfiguration.xml";

        static ServiceApiConfigurationService()
        {
            GetConfigurationFromXml();
        }

        public static ServiceApiConfigurationEntity ServiceConfiguration { get; private set; }


        private static void GetConfigurationFromXml()
        {
            var serializer = new XmlSerializer(typeof(ServiceApiConfigurationEntity));
            using (var stream = new FileStream(Directory.GetCurrentDirectory() + XmlFileConfigurationPath, FileMode.Open))
            {
                ServiceConfiguration = (ServiceApiConfigurationEntity)serializer.Deserialize(stream);
            }
        }
    }
}
