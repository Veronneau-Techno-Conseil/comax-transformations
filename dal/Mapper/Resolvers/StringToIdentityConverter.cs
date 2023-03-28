using AutoMapper;
using CommunAxiom.Transformations.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Mapper.Resolvers
{
    public class StringToIdentityConverter : ITypeConverter<string, IdentityRole>
    {
        public IdentityRole Convert(string source, IdentityRole destination, ResolutionContext context)
        {
            return new IdentityRole(source);
        }
    }
}
