using AutoMapper;
using CommunAxiom.Transformations.DAL.Mapper.Resolvers;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<string, IdentityRole>().ConvertUsing<StringToIdentityConverter>();
            CreateMap<IdentityRole, string>().ConvertUsing(s => s.ToString());
            CreateMap<Models.User, Contracts.User>();
            CreateMap<Contracts.User, Models.User>();
        }
    }
}
