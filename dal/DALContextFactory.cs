using CommunAxiom.Transformations.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL
{
    public class DALContextFactory : IDesignTimeDbContextFactory<Models.TransformationsDbContext>
    {
        public TransformationsDbContext CreateDbContext(string[] args)
        {
            var sc = new ServiceCollection();
            var ob = new DbContextOptionsBuilder<Models.TransformationsDbContext>();
            var cb = new ConfigurationBuilder();
            cb.AddJsonFile("./config.json");
            sc.AddSingleton<IConfiguration>(cb.Build());
            var configuration = cb.Build();
            sc.Configure<Config>(c => configuration.Bind("DbConfig", c));
            sc.AddDbContext<Models.TransformationsDbContext>(ServiceLifetime.Transient, ServiceLifetime.Scoped);
            var provider = sc.BuildServiceProvider();
            return provider.GetService<TransformationsDbContext>();
        }
    }
}
