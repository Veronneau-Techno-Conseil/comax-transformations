using AutoMapper;
using CommunAxiom.Transformations.AppModel.Repositories;
using CommunAxiom.Transformations.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IMapper mapper;

        public UserRepository(IServiceProvider serviceProvider, IMapper mapper)
        {
            this.serviceProvider = serviceProvider;
            this.mapper = mapper;
        }
        public Task<User> Create(User user)
        {
            return this.serviceProvider.WithContext(async x =>
            {
                var toadd = this.mapper.Map<Models.User>(user);
                toadd.LastConnected = DateTime.UtcNow;
                x.Set<Models.User>().Add(toadd);
                await x.SaveChangesAsync();
                return this.mapper.Map<Contracts.User>(toadd);
            });
        }

        public Task<bool> Exists(string username) =>
            this.serviceProvider.WithContext(x => x.Set<Models.User>().AnyAsync(x => x.Username == username));

        public Task<User> Get(string username)=>
            this.serviceProvider.WithContext(async x => this.mapper.Map<Contracts.User>(await x.Set<Models.User> ().FirstAsync(x => x.Username == username)));


        public Task Stamp(User user)
        {
            return this.serviceProvider.WithContext(async x =>
            {
                var set = x.Set<Models.User>();
                var toupdate = await set.FirstAsync(x => x.Username == user.Username);
                toupdate.LastConnected = DateTime.UtcNow;
                await x.SaveChangesAsync();
            });
        }
    }
}
