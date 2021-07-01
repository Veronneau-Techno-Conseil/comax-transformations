
using CommunAxiom.Transformations.AppModel;
using CommunAxiom.Transformations.AppModel.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALTests
{
    [TestClass]
    public sealed class Setup
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public static IServiceProvider ServiceProvider { get; private set; }

        [AssemblyInitialize]
        public static void AssemblySetup(TestContext context)
        {
            ServiceCollection serviceCollections = new ServiceCollection();
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("./config.json");
            Configuration = configurationBuilder.Build();
            serviceCollections.AddSingleton<IConfigurationRoot>(Configuration);
            serviceCollections.AddSingleton<IConfiguration>(Configuration);
            CommunAxiom.Transformations.DAL.Setup.Configure("DbConfig", serviceCollections, Configuration);

            ServiceProvider = serviceCollections.BuildServiceProvider();
        }
    }
}
