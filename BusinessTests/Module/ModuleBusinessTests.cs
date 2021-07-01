using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommunAxiom.Transformations.Business.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunAxiom.Transformations.AppModel.Business;
using Microsoft.Extensions.DependencyInjection;
using CommunAxiom.Transformations.Contracts;


namespace BusinessTests.ModuleTests
{
    [TestClass()]
    public class ModuleBusinessTests
    {
        
        [TestMethod()]
        public async Task AddModuleTest()
        {
            var mb = Setup.ServiceProvider.GetService<IModuleBusiness>();
            var res = await mb.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Some_Module",
                Creator = "best@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON,
                Created = DateTime.UtcNow
            });

            Assert.IsNotNull(res.ReturnValue);
            Assert.IsTrue(res.ValidationResult.IsValid);

            res = await mb.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Some module",
                Creator = "best@pirates.carib",
                ModuleTypeCode = "ERROR!!",
                Created = DateTime.UtcNow
            });

            Assert.IsNull(res.ReturnValue);
            Assert.IsFalse(res.ValidationResult.IsValid);
            Assert.IsTrue(res.ValidationResult.Errors.Count > 0);
        }

        [TestMethod()]
        public async Task DeleteModuleTest()
        {
            var mb = Setup.ServiceProvider.GetService<IModuleBusiness>();
            var res = await mb.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Some_Module",
                Creator = "best@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON,
                Created = DateTime.UtcNow
            });

            Assert.IsNotNull(res.ReturnValue);
            Assert.IsTrue(res.ValidationResult.IsValid);
            Assert.IsTrue(res.ReturnValue.Id > 0);

            var vr = await mb.DeleteModule(res.ReturnValue.Id);
            Assert.IsTrue(vr.IsValid);

            vr = await mb.DeleteModule(res.ReturnValue.Id);
            Assert.IsFalse(vr.IsValid);
            Assert.IsTrue(vr.Errors.Count > 0);

        }

        [TestMethod()]
        public async Task GetModuleTest()
        {
            var mb = Setup.ServiceProvider.GetService<IModuleBusiness>();
            var res = await mb.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Some_Module",
                Creator = "best@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON,
                Created = DateTime.UtcNow
            });

            Assert.IsNotNull(res.ReturnValue);
            Assert.IsTrue(res.ValidationResult.IsValid);
            Assert.IsTrue(res.ReturnValue.Id > 0);

            var module = await mb.GetModule(res.ReturnValue.Id);
            Assert.IsNotNull(res.ReturnValue);

        }

        [TestMethod()]
        public async Task ListModuleTest()
        {
            var mb = Setup.ServiceProvider.GetService<IModuleBusiness>();
            var res = await mb.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Some_Module",
                Creator = "best@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON,
                Created = DateTime.UtcNow
            });

            Assert.IsNotNull(res.ReturnValue);
            Assert.IsTrue(res.ValidationResult.IsValid);
            Assert.IsTrue(res.ReturnValue.Id > 0);

            var modules = await mb.ListModule("").ToListAsync();
            Assert.IsTrue(modules.Count > 0);
        }

        [TestMethod()]
        public async Task UpdateModuleTest()
        {
            var mb = Setup.ServiceProvider.GetService<IModuleBusiness>();
            var res = await mb.AddModule(new CommunAxiom.Transformations.Contracts.Module
            {
                Code = "Some_Module",
                Creator = "best@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON,
                Created = DateTime.UtcNow
            });

            Assert.IsNotNull(res.ReturnValue);
            Assert.IsTrue(res.ValidationResult.IsValid);

            res.ReturnValue.Code = "NewCode";

            res = await mb.UpdateModule(res.ReturnValue);

            Assert.IsNotNull(res.ReturnValue);
            Assert.IsTrue(res.ValidationResult.IsValid);

            res.ReturnValue.ModuleTypeCode = "ERROR!!";
            res = await mb.UpdateModule(res.ReturnValue);

            Assert.IsNull(res.ReturnValue);
            Assert.IsFalse(res.ValidationResult.IsValid);
            Assert.IsTrue(res.ValidationResult.Errors.Count > 0);
        }
    }
}