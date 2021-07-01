using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.Contracts
{
    public class Result<IResultType>
    {
        public IResultType ReturnValue { get; set; }
        public FluentValidation.Results.ValidationResult ValidationResult { get; set; }
    }
}
