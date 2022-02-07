using AutoMapper;
using CommunAxiom.Transformations.DAL.Mapper.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Mapper
{
    public class ModuleProfile: Profile
    {
        public ModuleProfile()
        {
            CreateMap<string, Version>().ConvertUsing<StringToVersionConverter>();
            CreateMap<Version, string>().ConvertUsing(s => s.ToString());
            CreateMap<Models.Module, Contracts.Module>()
                .ForMember(x => x.Creator, m => m.MapFrom(n => n.Creator.Username))
                .ForMember(x => x.ModuleTypeCode, m => m.MapFrom(x => x.ModuleType.Code));

            CreateMap<Contracts.Module, Models.Module>()
                .ForMember(x => x.ModuleTypeId, x => x.ConvertUsing<ModuleTypeToIdConverter, string>(x => x.ModuleTypeCode))
                .ForMember(x => x.ModuleType, x => x.Ignore())
                .ForMember(x => x.CreatorId, x => x.ConvertUsing<UserToIdConverter, string>(x => x.Creator))
                .ForMember(x => x.Creator, x => x.Ignore());
                
        }
    }
}
