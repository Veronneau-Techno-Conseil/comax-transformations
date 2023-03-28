using CommunAxiom.Transformations.AppModel.Repositories;
using CommunAxiom.Transformations.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL
{
    public static class Setup
    {
        public static string ConfigName { get; private set; }
        public static void Configure(string configName, IServiceCollection serviceCollection, IConfiguration configuration)
        {
            Setup.ConfigName = configName;

            serviceCollection.Configure<Config>(x => configuration.GetSection(configName).Bind(x));

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IModuleRepository, ModuleRepository>();
            
            serviceCollection.AddDbContext<Models.TransformationsDbContext>(ServiceLifetime.Transient, ServiceLifetime.Transient);
            serviceCollection.AddAutoMapper(typeof(Setup).Assembly);

            UpgradeDb(serviceCollection.BuildServiceProvider());
        }

        public static void UpgradeDb(ServiceProvider serviceProvider)
        {
            var val = serviceProvider.GetService<IOptionsMonitor<Config>>();
            var cfg = val.CurrentValue;
            serviceProvider.WithContext(ctxt =>
            {
                //if (!cfg.MemoryDb)
                //{
                    //if (cfg.ShouldDrop)
                        //ctxt.Database.EnsureDeleted();
                    //ctxt.Database.EnsureCreated();
                    //ctxt.Database.Migrate();
                //}
                Seed.ModuleTypes.Seed(ctxt);
            });
        }
    }
}
