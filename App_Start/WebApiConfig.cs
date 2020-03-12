using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Assignment1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
			config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
			config.Formatters.JsonFormatter
			.SerializerSettings
			.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			config.Routes.MapHttpRoute("DataRefApi", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
			config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
		}
    }
}
