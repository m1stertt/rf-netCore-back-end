using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ScrumMasters.Webshop.Security;
using ScrumMasters.Webshop.Security.Model;

namespace ScrumMasters.Webshop.WebAPI.PolicyHandlers
{
    public class CanManageAccountHandler: AuthorizationHandler<CanManageAccountHandler>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanManageAccountHandler handler)
        {
            var defaultContext = context.Resource as DefaultHttpContext;
            if (defaultContext != null)
            {
                var user = defaultContext.Items["LoginUser"] as LoginUser;
                if (user != null)
                {
                    var authService = defaultContext.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                    var permissions = authService.GetPermissions(user.Id);
                    if (permissions.Exists(p => p.Name.Equals("CanManageAccount")))
                    {
                        context.Succeed(handler);
                    }
                    else
                    {
                        context.Fail();
                    }
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