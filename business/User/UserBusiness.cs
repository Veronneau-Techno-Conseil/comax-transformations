using CommunAxiom.Transformations.AppModel.Business;
using CommunAxiom.Transformations.AppModel.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.Business.User
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository userRepository;
        private readonly IValidator<Contracts.User> validator;

        public UserBusiness(IUserRepository userRepository, IValidator<Contracts.User> validator)
        {
            this.userRepository = userRepository;
            this.validator = validator;
        }

        public async Task EnsureCreated(Contracts.User user)
        {
            if (!await this.userRepository.Exists(user.Username))
            {
                var res = await this.validator.ValidateAsync(user);
                if (!res.IsValid)
                {
                    throw new ValidationException("Error validating user information", res.Errors);
                }
                await this.userRepository.Create(user);
            }
            else
            {
                await this.userRepository.Stamp(user);
            }
        }

        public Task<Contracts.User> GetUser(string username) =>
            this.userRepository.Get(username);
    }
}
