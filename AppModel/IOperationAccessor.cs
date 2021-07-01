using CommunAxiom.Transformations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.AppModel
{
    public interface IOperationAccessor
    {
        OperationType GetCurrentOperationType();
        void SetCurrentOperation(OperationType operationType);
        void Reset();
    }
}
