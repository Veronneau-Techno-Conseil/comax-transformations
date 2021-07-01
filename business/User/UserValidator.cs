using CommunAxiom.Transformations.AppModel.Repositories;
using CommunAxiom.Transformations.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.Business.User
{
    public class UserValidator : AbstractValidator<Contracts.User>
    {
        private readonly IUserRepository userRepository = null;
        public UserValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

            RuleFor(x => x.Name).NotEmpty().WithErrorCode(ERROR_CODES.MANDATORY).DependentRules(() =>
            {
                RuleFor(x => x.Name).MinimumLength(3).WithErrorCode(ERROR_CODES.MIN_LEN);
            });

            RuleFor(x => x.Username).NotEmpty().WithErrorCode(ERROR_CODES.MANDATORY).DependentRules(() =>
            {
                RuleFor(x => x.Username).MinimumLength(3).WithErrorCode(ERROR_CODES.MIN_LEN);
                RuleFor(x => x.Username).Matches(@"^[0-9a-zA-Z@\.\-_]{3,}$").WithErrorCode(ERROR_CODES.REGEX);
                RuleFor(x => x.Username).MustAsync((username, token) => NotExists(username)).WithErrorCode(ERROR_CODES.UK_EXISTS);
            });
            
        }

        private async Task<bool> NotExists(string username)
        {
            return !await this.userRepository.Exists(username);
        }
    }
}
