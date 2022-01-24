
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql.EntityFrameworkCore;

namespace CommunAxiom.Transformations.DAL.Models.Configurations
{
    public class InitialConfig : IModelConfig
    {
        public void SetupFields(ModelBuilder builder)
        {
            builder.Entity<Module>()
                .HasKey(x => x.Id);
            builder.Entity<Module>()
                .Property(x => x.Id)
                .UseIdentityColumn();

            builder.Entity<Module>()
                .HasIndex(x => x.Code)
                .IsUnique(true);
            builder.Entity<Module>()
                .Property(x => x.Code)
                .IsRequired();
            builder.Entity<Module>()
                .Property(x => x.Created)
                .IsRequired();
            builder.Entity<Module>()
                .Property(x => x.CreatorId)
                .IsRequired();
            builder.Entity<Module>()
                .Property(x => x.ModuleTypeId)
                .IsRequired();
            builder.Entity<Module>()
                .Property(x => x.Version)
                .IsRequired();
            builder.Entity<Module>()
                .Property(x => x.Depreciation)
                .IsRequired();

            builder.Entity<User>()
                .HasKey(x => x.Id);
            builder.Entity<User>()
                .Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn();
            builder.Entity<User>()
                .Property(x => x.Name)
                .IsRequired();
            builder.Entity<User>()
                .Property(x => x.Username)
                .IsRequired();

            builder.Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();
        }

        public void SetupRelationships(ModelBuilder builder)
        {
            builder.Entity<Module>()
                .HasOne(x => x.Creator)
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .HasPrincipalKey(x=>x.Id);

            builder.Entity<Module>()
                .HasOne(x => x.ModuleType)
                .WithMany()
                .HasForeignKey(x => x.ModuleTypeId)
                .HasPrincipalKey(x => x.Id);
        }

        public void SetupTables(ModelBuilder builder)
        {
            builder.Entity<Module>()
                .ToTable("Modules");
            builder.Entity<User>()
                .ToTable("Users");
            builder.Entity<ModuleType>()
                .ToTable("ModuleTypes");

        }
    }
}
