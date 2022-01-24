using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int ModuleTypeId { get; set; }
        public ModuleType ModuleType { get; set; }

        //TODO: uncomment when adding translations
        //[Required]
        //public int TranslationId { get; set; }
        
        public DateTime Created { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public double Version { get; set; } = 1.0;
        public DateTime Depreciation { get; set; }
    }
}
