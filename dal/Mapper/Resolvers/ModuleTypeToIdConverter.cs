using AutoMapper;
using CommunAxiom.Transformations.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Mapper.Resolvers
{
    public class ModuleTypeToIdConverter : IValueConverter<string, int>
    {
        private readonly IServiceProvider provider = null;

        public ModuleTypeToIdConverter(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public int Convert(string sourceMember, ResolutionContext context)
        {
            return provider.WithContext(x => x.Set<ModuleType>().FirstOrDefault(x => x.Code == sourceMember).Id);
        }

    }
}
