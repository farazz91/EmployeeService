using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EmployeeService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:57203/","","");  //Specifically making one origin accessable

            //EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");  //making all accessable origin,header,methods

            //config.EnableCors(cors);

            config.EnableCors();
        }
    }
}
