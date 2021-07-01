using AutoMapper;
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
            CreateMap<Models.User, Contracts.User>();
            CreateMap<Contracts.User, Models.User>();
        }
    }
}
