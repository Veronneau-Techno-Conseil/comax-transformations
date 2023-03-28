using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.Contracts
{
    public class Module
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public IFormFile Contents { get; set; }
        public string Hash { get; set; }
        public string ModuleTypeCode { get; set; }
        public DateTime Created { get; set; }
        public string Creator { get; set; }
        public Version VersionModule { get; set; }
        public DateTime Depreciation { get; set; }
    }
}
