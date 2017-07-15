using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SimpleExcample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PaginationApi",
                routeTemplate: "api/{controller}/{action}/{maxCustomerPerPage}/{currentPage}",
                defaults: new {action = "GetPagination", maxCustomerPerPage = "3", currentPage = "1" }
            );
        }
    }
}
