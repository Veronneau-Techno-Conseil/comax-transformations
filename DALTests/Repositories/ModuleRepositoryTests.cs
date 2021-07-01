using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommunAxiom.Transformations.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CommunAxiom.Transformations.AppModel.Repositories;
using Microsoft.Extensions.DependencyInjection;
using CommunAxiom.Transformations.Contracts;


namespace DALTests
{
    [TestClass()]
    public class ModuleRepositoryTests
    {
        private async Task EnsureUserCreation()
        {
            var userRepo = Setup.ServiceProvider.GetService<IUserRepository>();
            if (!await userRepo.Exists("best1@pirates.carib"))
            {
                var user = await userRepo.Create(new CommunAxiom.Transformations.Contracts.User
                {
                    Username = "best1@pirates.carib",
                    Name = "Jack Sparrow",
                    LastConnected = DateTime.UtcNow
                });
            }
        }

        [TestMethod()]
        public async Task AddModuleTest()
        {
            await EnsureUserCreation();
            var repo = Setup.ServiceProvider.GetService<IModuleRepository>();
            var module = await repo.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Code1",
                Created = DateTime.UtcNow,
                Creator = "best1@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON
            });

            Assert.IsNotNull(module);
            Assert.IsTrue(module.Id > 0);
        }

        [TestMethod()]
        public async Task DeleteModuleTest()
        {
            await EnsureUserCreation();

            var repo = Setup.ServiceProvider.GetService<IModuleRepository>();
            var module = await repo.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Code2",
                Created = DateTime.UtcNow,
                Creator = "best1@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON
            });

            Assert.IsNotNull(module);
            Assert.IsTrue(module.Id > 0);

            var m = await repo.GetModuleByCode("Code2");
            Assert.IsNotNull(m);
            await repo.DeleteModule(m.Id);
        }

        [TestMethod()]
        public async Task GetModuleTest()
        {
            await EnsureUserCreation();

            var repo = Setup.ServiceProvider.GetService<IModuleRepository>();
            var module = await repo.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Code4",
                Created = DateTime.UtcNow,
                Creator = "best1@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON
            });

            Assert.IsNotNull(module);
            Assert.IsTrue(module.Id > 0);

            var m = await repo.GetModule(module.Id);
            Assert.IsNotNull(m);
        }

        [TestMethod()]
        public async Task GetModuleByCodeTest()
        {
            await EnsureUserCreation();

            var repo = Setup.ServiceProvider.GetService<IModuleRepository>();
            var module = await repo.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Code3",
                Created = DateTime.UtcNow,
                Creator = "best1@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON
            });

            Assert.IsNotNull(module);
            Assert.IsTrue(module.Id > 0);

            var m = await repo.GetModuleByCode("Code3");
            Assert.IsNotNull(m);
        }

        [TestMethod()]
        public async Task GetModuleTypesTest()
        {
            var repo = Setup.ServiceProvider.GetService<IModuleRepository>();
            var mtList = await repo.GetModuleTypes().ToListAsync();
            Assert.IsNotNull(mtList);
            Assert.IsTrue(mtList.Count > 0);
        }

        [TestMethod()]
        public async Task ListModuleTest()
        {
            await EnsureUserCreation();

            var repo = Setup.ServiceProvider.GetService<IModuleRepository>();
            var module = await repo.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Code5",
                Created = DateTime.UtcNow,
                Creator = "best1@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON
            });

            Assert.IsNotNull(module);
            Assert.IsTrue(module.Id > 0);

            module = await repo.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Code6",
                Created = DateTime.UtcNow,
                Creator = "best1@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON
            });

            Assert.IsNotNull(module);
            Assert.IsTrue(module.Id > 0);

            var lst = await repo.ListModule("").ToListAsync();
            Assert.IsNotNull(lst);
            Assert.IsTrue(lst.Count > 1);
        }

        [TestMethod()]
        public async Task ModuleExistsTest()
        {
            await EnsureUserCreation();

            var repo = Setup.ServiceProvider.GetService<IModuleRepository>();
            var module = await repo.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Code7",
                Created = DateTime.UtcNow,
                Creator = "best1@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON
            });

            Assert.IsNotNull(module);
            Assert.IsTrue(module.Id > 0);

            Assert.IsTrue(await repo.ModuleExists("Code7"));
            Assert.IsFalse(await repo.ModuleExists("Code99"));
        }

        [TestMethod()]
        public async Task ModuleIdExistsTest()
        {
            await EnsureUserCreation();

            var repo = Setup.ServiceProvider.GetService<IModuleRepository>();
            var module = await repo.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Code8",
                Created = DateTime.UtcNow,
                Creator = "best1@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON
            });

            Assert.IsNotNull(module);
            Assert.IsTrue(module.Id > 0);

            Assert.IsTrue(await repo.ModuleIdExists(module.Id));
            Assert.IsFalse(await repo.ModuleIdExists(999));
        }

        [TestMethod()]
        public async Task ModuleTypeExistsTest()
        {
            await EnsureUserCreation();

            var repo = Setup.ServiceProvider.GetService<IModuleRepository>();
            Assert.IsTrue(await repo.ModuleTypeExists(ModuleType.PYTHON));
            Assert.IsFalse(await repo.ModuleTypeExists("Doesn't exist"));
        }

        [TestMethod()]
        public async Task UpdateModuleTest()
        {
            await EnsureUserCreation();

            var repo = Setup.ServiceProvider.GetService<IModuleRepository>();
            var module = await repo.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Code9",
                Created = DateTime.UtcNow,
                Creator = "best1@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON
            });

            Assert.IsNotNull(module);
            Assert.IsTrue(module.Id > 0);

            module.Code = "Code10";

            var module1 = await repo.UpdateModule(module);

            Assert.IsNotNull(module1);
            Assert.AreEqual("Code10", module1.Code);

        }
    }
}