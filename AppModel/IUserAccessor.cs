using CommunAxiom.Transformations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.AppModel
{
    public interface IUserAccessor
    {
        string GetCurrentUsername();
        Task<User> GetUser();
    }
}
