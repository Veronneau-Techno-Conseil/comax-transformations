using FluentValidation;

using CommunAxiom.Transformations.Contracts;
using CommunAxiom.Transformations.AppModel.Repositories;
using FluentValidation.Results;
using CommunAxiom.Transformations.AppModel;
using System.Threading.Tasks;
using System;

namespace CommunAxiom.Transformations.Business.Module
{
    public class ModuleValidator : AbstractValidator<Contracts.Module>
    {
        private readonly IModuleRepository moduleRepository;
        private readonly IUserRepository userRepository;
        private readonly IOperationAccessor operationAccessor;

        public ModuleValidator(IModuleRepository moduleRepository, IUserRepository userRepository, IOperationAccessor operationAccessor)
        {
            this.moduleRepository = moduleRepository;
            this.userRepository = userRepository;
            this.operationAccessor = operationAccessor;
            
            RuleFor(x => x.Id).MustAsync((id, token) => Exists(id)).WithErrorCode(ERROR_CODES.NOT_FOUND);
            RuleFor(x => x.Code).NotEmpty().WithErrorCode(ERROR_CODES.MANDATORY).DependentRules(() =>
            {
                RuleFor(x => x.Code).MinimumLength(4).WithErrorCode(ERROR_CODES.MIN_LEN);
                RuleFor(x => x.Code).Matches("^[a-zA-Z0-9-_]{4,}$").WithErrorCode(ERROR_CODES.REGEX).WithMessage(Resources.Messages.AllowedCodes);
                RuleFor(x => x).MustAsync((code, token) => NotExists(code)).WithErrorCode(ERROR_CODES.UK_EXISTS);
            });

            RuleFor(x => x.Creator).NotNull().WithErrorCode(ERROR_CODES.MANDATORY).DependentRules(()=>{
                RuleFor(x => x.Creator).MustAsync((name, token) => this.userRepository.Exists(name)).WithErrorCode(ERROR_CODES.FK);
            });

            RuleFor(x => x.ModuleTypeCode).NotNull().WithErrorCode(ERROR_CODES.MANDATORY).DependentRules(() =>
            {
                RuleFor(x => x.ModuleTypeCode).MustAsync((mt, token) => this.moduleRepository.ModuleTypeExists(mt)).WithErrorCode(ERROR_CODES.FK);
            });

            RuleFor(x => x.Depreciation).NotNull().WithErrorCode(ERROR_CODES.MANDATORY).DependentRules(() =>
            {
                RuleFor(x => x.Depreciation).Must((date, token) => Validate_Date(date)).WithErrorCode(ERROR_CODES.MIN_DATE);
            });
        }

        private async Task<bool> Exists(int moduleId)
        {
            var co = operationAccessor.GetCurrentOperationType();

            switch (co)
            {
                case OperationType.CREATE:
                    return true;
                case OperationType.UPDATE:
                case OperationType.DELETE:
                    var exists = await this.moduleRepository.ModuleIdExists(moduleId);
                    return exists;
            }

            return true;
        }

        private async Task<bool> NotExists(Contracts.Module module)
        {
            var co = operationAccessor.GetCurrentOperationType();

            var exists = await this.moduleRepository.ModuleExists(module.Code);
            switch (co)
            {
                case OperationType.CREATE:
                    return !exists;
                case OperationType.UPDATE:
                    if (!exists)
                        return true;
                    var current = await this.moduleRepository.GetModuleByCode(module.Code);
                    return current.Id == module.Id;
            }
            
            return true;
        }

        protected override bool PreValidate(ValidationContext<Contracts.Module> context, ValidationResult result)
        {
            
            return base.PreValidate(context, result);
        }

        private bool Validate_Date(Contracts.Module module)
        {
            if(DateTime.Compare(module.Depreciation, module.Created) > 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
