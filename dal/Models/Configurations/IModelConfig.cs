﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Models.Configurations
{
    public interface IModelConfig
    {
        void SetupTables(ModelBuilder builder);
        void SetupFields(ModelBuilder builder);
        void SetupRelationships(ModelBuilder builder);
    }
}
