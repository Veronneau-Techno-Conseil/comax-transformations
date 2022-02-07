using CommunAxiom.Transformations.AppModel.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity;

namespace web
{
    public static class SeedTestData
    {
        public static async Task Execute(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userBusiness = scope.ServiceProvider.GetService<IUserBusiness>();
            var user = new CommunAxiom.Transformations.Contracts.User { Name = "Test User", Username = "testUser@test.com", role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole("Administrator") };
            await userBusiness.EnsureCreated(user);

        }
    }
}
