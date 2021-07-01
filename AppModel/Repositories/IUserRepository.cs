using CommunAxiom.Transformations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.AppModel.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Exists(string username);
        Task<User> Create(User user);
        Task Stamp(User user);
        Task<User> Get(string username);
    }
}
