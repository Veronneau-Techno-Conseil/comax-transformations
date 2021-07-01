using CommunAxiom.Transformations.AppModel;
using CommunAxiom.Transformations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Helpers
{
    public class OperationAccessor : IOperationAccessor
    {
        private OperationType operationType = OperationType.NOT_SET;


        public OperationType GetCurrentOperationType()
        {
            return this.operationType;
        }

        public void Reset()
        {
            this.operationType = OperationType.NOT_SET;
        }

        public void SetCurrentOperation(OperationType operationType)
        {
            this.operationType = operationType;
        }
    }
}