using CommunAxiom.Transformations.Contracts;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace CommunAxiom.Transformations.AppModel.Business
{
    public interface IModuleBusiness
    {
        IAsyncEnumerable<Module> ListModule(string search);
        Task<Module> GetModule(int id);
        Task<Result<Module>> AddModule(Module id);
        Task<Result<Module>> UpdateModule(Module id);
        Task<ValidationResult> DeleteModule(int id); 
    }
}
