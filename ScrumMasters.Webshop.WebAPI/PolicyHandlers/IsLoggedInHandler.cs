using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ScrumMasters.Webshop.Security.Model;

namespace ScrumMasters.Webshop.WebAPI.PolicyHandlers
{
    public class IsLoggedInHandler: AuthorizationHandler<IsLoggedInHandler>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsLoggedInHandler handler)
        {
            var defaultContext = context.Resource as DefaultHttpContext;
            if (defaultContext != null)
            {
                var user = defaultContext.Items["LoginUser"] as LoginUser;
                if (user != null){context.Succeed(handler);}
                else{ context.Fail(); }
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}