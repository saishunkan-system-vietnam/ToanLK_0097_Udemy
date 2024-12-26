using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingSection.CustomRouteConstraints
{
    public class MonthRouteConstraint : IRouteConstraint
    {
        enum Months
        {
            January, February, March, April, May, June, July, August, September, October, November, December
        }
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (Enum.TryParse(typeof(Months), (string)values["value"], true, out _))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
