using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace RazorWebAppProject.Security
{
    public class TestConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (int.TryParse(values["Id"].ToString(), out int result))
            {
                if(result % 2 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
