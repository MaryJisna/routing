
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace routing.CustomConstraint
{
    public class Aiphanumerics : IRouteConstraint
    {
        public bool Match(
            HttpContext? httpContext, 
            IRouter? route, 
            string routeKey, 
            RouteValueDictionary values, 
            RouteDirection routeDirection)
        {
            if(!values.ContainsKey(routeKey))
            {
                return false;
            }
            Regex regex = new Regex("[a-zA-Z][a-zA-Z0-9]*$");
            string? usernameValue =Convert.ToString(values[routeKey]);
            if(regex.IsMatch(usernameValue))
            {
                return true;
            }
            return false;
        }
    }
}
