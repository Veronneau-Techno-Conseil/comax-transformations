using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace web
{
	public class UserAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>
    {

        protected override Task HandleRequirementAsync(
                                      AuthorizationHandlerContext context,
                            OperationAuthorizationRequirement requirement)
        {
            List<string> things_users_cant_do = new List<string>();
            things_users_cant_do.Add("Create");
            things_users_cant_do.Add("Delete");

            if (context.User == null)
            {
                return Task.CompletedTask;
            }
            if (context.User.IsInRole("User"))
            {
                if (things_users_cant_do.Contains(requirement.Name))
                {
                    return Task.CompletedTask;
                }
                else
                {
                    context.Succeed(requirement);
                }
            }
                
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}