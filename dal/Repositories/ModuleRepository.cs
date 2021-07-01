using AutoMapper;
using CommunAxiom.Transformations.AppModel.Repositories;
using CommunAxiom.Transformations.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly IServiceProvider serviceProvider = null;
        private readonly IMapper mapper = null;

        private static Func<Models.TransformationsDbContext, string, IAsyncEnumerable<Models.Module>> SearchModules =
            EF.CompileAsyncQuery<Models.TransformationsDbContext, string, Models.Module>(
                (ctxt, search) =>
                    from module in ctxt.Set<Models.Module>().Include(x=>x.ModuleType).Include(x=>x.Creator)
                    where module.Code.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                    select module
                );

        public ModuleRepository(IServiceProvider serviceProvider, IMapper mapper)
        {
            this.serviceProvider = serviceProvider;
            this.mapper = mapper;
        }

        public Task<Module> AddModule(Module m)
        {
            return this.serviceProvider.WithContext(async x =>
            {
                var toadd = this.mapper.Map<Models.Module>(m);
                x.Set<Models.Module>().Add(toadd);
                await x.SaveChangesAsync();
                await LoadProperties(x, toadd);
                return this.mapper.Map<Contracts.Module>(toadd);
            });
        }

        public Task DeleteModule(int id)
        {
            return this.serviceProvider.WithContext(async x =>
            {
                var set = x.Set<Models.Module>();
                var todel = await set.FindAsync(id);
                set.Remove(todel);
                await x.SaveChangesAsync();
            });
        }

        public Task<Module> GetModule(int id)
        {
            return this.serviceProvider.WithContext(async x =>
            {
                var set = x.Set<Models.Module>();
                var value = await set.FindAsync(id);
                await LoadProperties(x, value);
                return this.mapper.Map<Contracts.Module>(value);
            });
        }

        public Task<Module> GetModuleByCode(string code)
        {
            return this.serviceProvider.WithContext(async x =>
            {
                var set = x.Set<Models.Module>();
                return this.mapper.Map<Contracts.Module>(
                    await set.Include(x => x.ModuleType).Include(x => x.Creator).FirstOrDefaultAsync(x => x.Code == code));
            });
        }

        public IAsyncEnumerable<ModuleType> GetModuleTypes()
        {
            return this.serviceProvider.WithContext(x => x.Set<Models.ModuleType>().AsAsyncEnumerable().Map(m => this.mapper.Map<Contracts.ModuleType>(m)));
        }

        public IAsyncEnumerable<Module> ListModule(string search)
        {
            return this.serviceProvider.WithContext(x=> SearchModules(x, search).Map(m=> this.mapper.Map<Contracts.Module>(m)));
        }

        public Task<bool> ModuleExists(string code) =>
            this.serviceProvider.WithContext(x => x.Set<Models.Module>().AnyAsync(y => y.Code == code));


        public Task<bool> ModuleIdExists(int id) =>
            this.serviceProvider.WithContext(x => x.Set<Models.Module>().AnyAsync(y => y.Id == id));

        public Task<bool> ModuleTypeExists(string code)=>
            this.serviceProvider.WithContext(x => x.Set<Models.ModuleType>().AnyAsync(y => y.Code == code));


        public Task<Module> UpdateModule(Module module)
        {
            return this.serviceProvider.WithContext(async ctxt =>
            {
                var m = await ctxt.Set<Models.Module>().FindAsync(module.Id);
                m = this.mapper.Map<Contracts.Module, Models.Module>(module, m);
                await ctxt.SaveChangesAsync();
                await LoadProperties(ctxt, m);
                return this.mapper.Map<Contracts.Module>(m);
            });
        }

        private async Task LoadProperties(Models.TransformationsDbContext ctxt, Models.Module module)
        {
            var entity = ctxt.Entry(module);
            await entity.Reference(x => x.Creator).LoadAsync();
            await entity.Reference(x => x.ModuleType).LoadAsync();
        }
    }
}
