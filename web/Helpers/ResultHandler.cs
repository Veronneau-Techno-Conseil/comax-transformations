using CommunAxiom.Transformations.Contracts;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CommunAxiom.Transformations.AppModel;

namespace web.Helpers
{
    public static class ResultHandler
    {
        public static async Task<ActionResult> HandleResult(this Controller controller, OperationType operationType, 
            Func<Task<ActionResult>> execute, Func<Exception, ActionResult> manageError) 
        {
            var oa = controller.HttpContext.RequestServices.GetService<IOperationAccessor>();
            oa.SetCurrentOperation(operationType);
            try
            {
                return await execute();
            }
            catch(Exception e)
            {
                return manageError(e);
            }
            finally
            {
                oa.Reset();
            }
        }
    }
}
