
using CommunAxiom.Transformations.DAL.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Models
{
    public class TransformationsDbContext : DbContext
    {
        internal readonly Config configs;

        public TransformationsDbContext(IOptionsMonitor<Config> options): base()
        {
            this.configs = options.CurrentValue;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (true) //configs.MemoryDb)
            {
                optionsBuilder.UseInMemoryDatabase("TransormationsDb");
            }
            else
            {
                optionsBuilder.UseNpgsql(configs.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            DbConfiguration.Setup(builder);
        }
    }
}
