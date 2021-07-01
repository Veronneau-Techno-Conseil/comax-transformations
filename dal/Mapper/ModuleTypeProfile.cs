using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Mapper
{
    public class ModuleTypeProfile: Profile
    {
        public ModuleTypeProfile()
        {
            CreateMap<Models.ModuleType, Contracts.ModuleType>();
            CreateMap<Contracts.ModuleType, Models.ModuleType>();
        }
    }
}
