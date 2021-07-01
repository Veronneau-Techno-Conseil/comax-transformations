using CommunAxiom.Transformations.AppModel.Business;
using CommunAxiom.Transformations.AppModel.Repositories;
using CommunAxiom.Transformations.Contracts;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.Business.Module
{
    public class ModuleBusiness : IModuleBusiness
    {
        private readonly IValidator<Contracts.Module> validator = null;
        private readonly IModuleRepository moduleRepository = null;
        public ModuleBusiness(IValidator<Contracts.Module> validator, IModuleRepository moduleRepository)
        {
            this.validator = validator;
            this.moduleRepository = moduleRepository;
        }
        public async Task<Result<Contracts.Module>> AddModule(Contracts.Module module)
        {
            Result<Contracts.Module> result = new Result<Contracts.Module>();
            result.ValidationResult = await validator.ValidateAsync(module);
            if (result.ValidationResult.IsValid)
            {
                result.ReturnValue = await moduleRepository.AddModule(module);
            }
            return result;
        }

        public async Task<ValidationResult> DeleteModule(int id)
        {
            if(!await moduleRepository.ModuleIdExists(id))
            {
                return new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("Id", "Id not found")
                    {
                        ErrorCode = ERROR_CODES.NOT_FOUND
                    }
                });

            }
            var module = await moduleRepository.GetModule(id);
            var res = await validator.ValidateAsync(module);
            if (res.IsValid)
            {
                await moduleRepository.DeleteModule(id);
            }
            return res;
        }

        public async Task<Contracts.Module> GetModule(int id) => 
            await moduleRepository.GetModule(id);

        public IAsyncEnumerable<Contracts.Module> ListModule(string search) =>
            moduleRepository.ListModule(search);

        public async Task<Result<Contracts.Module>> UpdateModule(Contracts.Module module)
        {
            Result<Contracts.Module> result = new Result<Contracts.Module>();
            result.ValidationResult = await validator.ValidateAsync(module);
            if (result.ValidationResult.IsValid)
            {
                result.ReturnValue = await moduleRepository.UpdateModule(module);
            }
            return result;
        }
    }
}
