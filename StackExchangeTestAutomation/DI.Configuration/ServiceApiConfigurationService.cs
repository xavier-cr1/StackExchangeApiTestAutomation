namespace DI.Configuration
{
    using System.IO;
    using System.Xml.Serialization;

    public static class ServiceApiConfigurationService
    {
        private const string XmlFileConfigurationPath = @"\..\CrossLayer.Configuration\configuration\TestConfiguration.xml";

        static ServiceApiConfigurationService()
        {
            GetConfigurationFromXml();
        }

        public static ServiceApiConfigurationEntity Configuration { get; private set; }


        private static void GetConfigurationFromXml()
        {
            var serializer = new XmlSerializer(typeof(ServiceApiConfigurationEntity));
            using (var stream = new FileStream(Directory.GetCurrentDirectory() + XmlFileConfigurationPath, FileMode.Open))
            {
                Configuration = (ServiceApiConfigurationEntity)serializer.Deserialize(stream);
            }
        }
    }
}
