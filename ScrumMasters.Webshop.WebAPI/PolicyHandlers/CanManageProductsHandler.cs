using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ScrumMasters.Webshop.Security;
using ScrumMasters.Webshop.Security.Model;

namespace ScrumMasters.Webshop.WebAPI.PolicyHandlers
{
    public class CanManageProductsHandler: AuthorizationHandler<CanManageProductsHandler>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanManageProductsHandler handler)
        {
            var defaultContext = context.Resource as DefaultHttpContext;
            if (defaultContext?.Items["LoginUser"] is LoginUser user)
            {
                var authService = defaultContext.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                var permissions = authService.GetPermissions(user.Id);
                if (permissions.Exists(p => p.Name.Equals("CanManageProducts")))
                {
                    context.Succeed(handler);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}