using CommunAxiom.Transformations.AppModel;
using CommunAxiom.Transformations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTests.Mocks
{
    public class OperationAccessorMock : IOperationAccessor
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
