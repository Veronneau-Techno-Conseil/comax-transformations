using AutoMapper;
using CommunAxiom.Transformations.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Mapper.Resolvers
{
    public class UserToIdConverter : IValueConverter<string, int>
    {
        private readonly IServiceProvider serviceProvider = null;
        public UserToIdConverter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public int Convert(string sourceMember, ResolutionContext context)
        {
            return this.serviceProvider.WithContext(x => x.Set<User>().FirstOrDefault(y => y.Username == sourceMember).Id);
        }
    }
}
