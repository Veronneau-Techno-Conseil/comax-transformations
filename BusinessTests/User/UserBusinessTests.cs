using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommunAxiom.Transformations.Business.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunAxiom.Transformations.AppModel.Business;
using Microsoft.Extensions.DependencyInjection;
using CommunAxiom.Transformations.Contracts;

namespace BusinessTests.UserTests
{
    [TestClass()]
    public class UserBusinessTests
    {
        

        [TestMethod()]
        public async Task EnsureCreatedTest()
        {
            var userBusiness = Setup.ServiceProvider.GetService<IUserBusiness>();

            await userBusiness.EnsureCreated(new User()
            {
                Username = "test1@test.com",
                Name = "Tester testy",
            });

            try
            {
                await userBusiness.EnsureCreated(new User()
                {
                    Username = "te",
                    Name = "Tester testy",
                });

                Assert.Fail("EnsureCreated should have thrown an error.");
            }
            catch(Exception e)
            {

            }
        }

        [TestMethod()]
        public async Task GetUserTest()
        {
            var userBusiness = Setup.ServiceProvider.GetService<IUserBusiness>();

            var user = await userBusiness.GetUser("best@pirates.carib");

            Assert.IsNotNull(user);
            
        }
    }
}