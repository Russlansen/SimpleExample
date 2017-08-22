using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SimpleExample
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
                routeTemplate: "api/{controller}/{action}/{MaxCustomerPerPage}/{TotalPagesMax}/{CurrentPage}/{OrderBy}/{Order}",
                defaults: new {action = "GetPagination", MaxCustomerPerPage = "3", TotalPagesMax = "7", CurrentPage = "1", OrderBy = "Id", Order = "ASC" }
            );
        }
    }
}
