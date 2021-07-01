using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL
{
    public class Config
    {
        public bool MemoryDb { get; set; }
        public string ConnectionString { get; set; }
        public bool ShouldDrop { get; set; }
    }
}
