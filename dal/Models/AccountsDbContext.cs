
using CommunAxiom.Transformations.DAL.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Models
{
    public class AccountsDbContext : DbContext
    {
        static AccountsDbContext()
        {
        }
        public AccountsDbContext(): base()
        {

        }

        public AccountsDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
     
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            DbConfiguration.Setup(builder);
        }
    }
}
