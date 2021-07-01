﻿using System;
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
        public string ModuleTypeCode { get; set; }
        public DateTime Created { get; set; }
        public string Creator { get; set; }
    }
}
