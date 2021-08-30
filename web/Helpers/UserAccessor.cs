using CommunAxiom.Transformations.AppModel;
using CommunAxiom.Transformations.AppModel.Business;
using CommunAxiom.Transformations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Helpers
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IUserBusiness userBusiness;
        public UserAccessor(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        public string GetCurrentUsername()
        {
            return "testUser@test.com";
        }

        public async Task<User> GetUser()
        {
            return await this.userBusiness.GetUser(this.GetCurrentUsername());
        }
    }
}
