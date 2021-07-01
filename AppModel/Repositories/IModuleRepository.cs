using CommunAxiom.Transformations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.AppModel.Repositories
{
    public interface IModuleRepository
    {
        IAsyncEnumerable<Module> ListModule(string search);
        Task<Module> GetModule(int id);
        Task<Module> GetModuleByCode(string code);
        Task<Module> AddModule(Module module);
        Task<Module> UpdateModule(Module module);
        Task<bool> ModuleExists(string code);
        Task<bool> ModuleIdExists(int code);
        Task<bool> ModuleTypeExists(string code);
        IAsyncEnumerable<ModuleType> GetModuleTypes();
        Task DeleteModule(int id);
    }
}
