using CommunAxiom.Transformations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.AppModel.Business
{
    public interface IUserBusiness
    {
        Task EnsureCreated(User user);
        Task<User> GetUser(string username);
    }
}
