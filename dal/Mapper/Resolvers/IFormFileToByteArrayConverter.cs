using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Mapper.Resolvers
{
    public class IFormFileToByteArrayConverter : ITypeConverter<IFormFile, byte[]>
    {
        public byte[] Convert(IFormFile source, byte[] destination, ResolutionContext context)
        {
            if (source.Length > 0)
            {
                using (var memorystream = new MemoryStream())
                {
                    source.CopyTo(memorystream);
                    var file = memorystream.ToArray();
                    return file;
                }
            } else
            {
                return new byte[0];
            }
        }

    }
}
