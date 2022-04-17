using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Mapper.Resolvers
{
    public class ByteArrayToIFormFileConverter : ITypeConverter<byte[], IFormFile>
    {
        public IFormFile Convert(byte[] source, IFormFile destination, ResolutionContext context)
        {
            using (var memorystream = new MemoryStream())
            {
                IFormFile file = new FormFile(memorystream, 0, source.Length, "transformation", "transformation");
                return file;

            }
        }

    }
}
