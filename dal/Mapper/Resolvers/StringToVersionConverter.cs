using AutoMapper;
using CommunAxiom.Transformations.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Mapper.Resolvers
{
    public class StringToVersionConverter : ITypeConverter<string, Version>
    {
        public Version Convert(string source, Version destination, ResolutionContext context)
        {
            return Version.Parse(source);
        }

    }
}
