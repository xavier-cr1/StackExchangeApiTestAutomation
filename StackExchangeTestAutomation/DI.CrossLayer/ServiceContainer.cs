namespace DI.CrossModule
{
    using Autofac;
    using API.Service.Calls;
    using API.Service.Contracts;
    using DataSource.OpenXml.Contracts;
    using DataSource.OpenXml;

    public static class ServiceContainer
    {
        /// <summary>
        /// Android container.
        /// </summary>
        public static IContainer ServiceCallsContainer { get; private set; }

        /// <summary>
        /// Initializes the <see cref="AppContainer"/> class.
        /// </summary>
        static ServiceContainer()
        {
            AppServiceCallsContainer();
        }

        /// <summary>
        /// Add all the android pages here.
        /// </summary>
        private static void AppServiceCallsContainer()
        {
            var buildContainer = new ContainerBuilder();
            buildContainer.RegisterType<AnswersApiServices>().As<IAnswersApiServices>();

            // Repository
            buildContainer.RegisterType<DataSourceManager>().As<IDataSourceContracts>();

            ServiceCallsContainer = buildContainer.Build();
        }
    }
}
