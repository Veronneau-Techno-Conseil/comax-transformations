using CommunAxiom.Transformations.AppModel.Repositories;
using CommunAxiom.Transformations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTests.Mocks
{
    public class UserRepositoryMock : IUserRepository
    {
        public List<User> Users { get; private set; } = new List<User>();
        public UserRepositoryMock()
        {
            Users.Add(new User { Id = 1, LastConnected = DateTime.UtcNow, Name = "Jack Sparrow", Username = "best@pirates.carib" });
        }
        public Task<User> Create(User user)
        {
            user.LastConnected = DateTime.UtcNow;
            Users.Add(user);
            return Task.FromResult(user);
        }

        public Task<bool> Exists(string username)
        {
            return Task.FromResult(Users.Any(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase)));
        }

        public Task<User> Get(string username)
        {
            return Task.FromResult(Users.First(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase)));
        }

        public Task Stamp(User user)
        {
            var res = Users.First(x => x.Username.Equals(user.Username, StringComparison.InvariantCultureIgnoreCase));
            res.LastConnected = DateTime.UtcNow;
            return Task.CompletedTask;
        }
    }
}
