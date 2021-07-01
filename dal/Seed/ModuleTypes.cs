using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL.Seed
{
    public static class ModuleTypes
    {
        public static void Seed(Models.TransformationsDbContext transformationsDbContext)
        {
            var dbset = transformationsDbContext.Set<Models.ModuleType>();
            if(!dbset.Any(x=>x.Code == Contracts.ModuleType.PYTHON))
            {
                dbset.Add(new Models.ModuleType
                {
                    Code = Contracts.ModuleType.PYTHON
                });
            }

            transformationsDbContext.SaveChanges();
        }
    }
}
