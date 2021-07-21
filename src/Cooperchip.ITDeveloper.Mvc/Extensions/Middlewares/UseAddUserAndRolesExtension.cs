using Microsoft.AspNetCore.Builder;

namespace Cooperchip.ITDeveloper.Mvc.Extensions.Middlewares
{
    public static class UseAddUserAndRolesExtension
    {
        public static IApplicationBuilder UserAddUsersAndRoles(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DefaultUsersAndRolesMiddleware>();
        }
    }
}
