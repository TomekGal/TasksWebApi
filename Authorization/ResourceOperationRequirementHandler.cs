using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TasksWebApi.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, TasksWebApi.Domains.Task>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, TasksWebApi.Domains.Task task)
        {
            if (requirement.ResourceOperation==ResourceOperation.Read||requirement.ResourceOperation==ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId=int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (task.CreatedById==userId)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
