using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommunAxiom.Transformations.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunAxiom.Transformations.AppModel.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DALTests
{
    [TestClass()]
    public class UserRepositoryTests
    {

        [TestMethod()]
        public async Task CreateTest()
        {
            var userRepo = Setup.ServiceProvider.GetService<IUserRepository>();
            var user = await userRepo.Create(new CommunAxiom.Transformations.Contracts.User
            {
                Username = "best1@pirates.carib",
                Name = "Jack Sparrow",
                LastConnected = DateTime.UtcNow
            });

            Assert.IsNotNull(user);
        }

        [TestMethod()]
        public async Task ExistsTest()
        {
            await CreateTest();
            var userRepo = Setup.ServiceProvider.GetService<IUserRepository>();
            var res = await userRepo.Exists("best1@pirates.carib");
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public async Task GetTest()
        {
            await CreateTest();
            var userRepo = Setup.ServiceProvider.GetService<IUserRepository>();
            var res = await userRepo.Get("best1@pirates.carib");
            Assert.IsNotNull(res);
        }

        [TestMethod()]
        public async Task StampTest()
        {
            await CreateTest();
            var userRepo = Setup.ServiceProvider.GetService<IUserRepository>();
            var res = await userRepo.Get("best1@pirates.carib");
            await userRepo.Stamp(res);
        }
    }
}