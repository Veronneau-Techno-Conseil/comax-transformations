using CommunAxiom.Transformations.AppModel.Repositories;
using CommunAxiom.Transformations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTests.Mocks
{
    public class ModuleRepositoryMock : IModuleRepository
    {
        public List<Module> Modules = new List<Module>();

        public ModuleRepositoryMock()
        {
            AddModule(new Module
            {
                Code = "Exists",
                Creator = "best@pirates.carib",
                ModuleTypeCode = ModuleType.PYTHON,
                Created = DateTime.UtcNow
            });
        }

        public Task<Module> AddModule(Module m)
        {
            m.Id = Modules.Count + 1;
            this.Modules.Add(m);
            return Task.FromResult(m);
        }

        public Task DeleteModule(int id)
        {
            this.Modules.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;
        }

        public Task<Module> GetModule(int id)
        {
            return Task.FromResult(Modules.FirstOrDefault(x => x.Id == id));
        }

        public Task<Module> GetModuleByCode(string code)
        {
            return Task.FromResult(Modules.FirstOrDefault(x => x.Code == code));
        }

        public async IAsyncEnumerable<ModuleType> GetModuleTypes()
        {
            var arr = new ModuleType[]
            {
                new ModuleType{ Code = ModuleType.PYTHON, Id=1 }
            };

            foreach(var a in arr)
            {
                yield return await Task.FromResult(a);
            }
        }

        public async IAsyncEnumerable<Module> ListModule(string search)
        {
            foreach (var a in Modules.Where(x => x.Code.Contains(search, StringComparison.InvariantCultureIgnoreCase)))
            {
                yield return await Task.FromResult(a);
            }
        }

        public Task<bool> ModuleExists(string code)
        {
            return Task.FromResult(Modules.Any(x => x.Code.Contains(code, StringComparison.InvariantCultureIgnoreCase)));
        }

        public Task<bool> ModuleIdExists(int code)
        {
            return Task.FromResult(Modules.Any(x => x.Id == code));
        }

        public Task<bool> ModuleTypeExists(string code)
        {
            switch (code)
            {
                case ModuleType.PYTHON:
                    return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<Module> UpdateModule(Module id)
        {
            var fst = Modules.First(x => x.Id == id.Id);
            Modules.Remove(fst);
            Modules.Add(id);
            return Task.FromResult(id);
        }
    }
}
