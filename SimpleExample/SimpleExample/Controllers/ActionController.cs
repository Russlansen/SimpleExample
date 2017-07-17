using SimpleExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleExample.Controllers
{
    public class ActionController : ApiController
    {
        CustomerContext customerContext = new CustomerContext();

        [ActionName("GetPagination")]
        public PaginationHandler<Customer> GetPagination(int maxCustomerPerPage, int currentPage)
        {
            var handler = new QueryHandler("SELECT * FROM Customers ORDER BY id", maxCustomerPerPage, currentPage);
            return customerContext.GetCustomersForPagination<Customer>(handler);
        }

        public List<Customer> Get(int id)
        {
            var customerToList = new List<Customer>();
            customerToList.Add(customerContext.Get(id));
            return customerToList;
        }

        public void Post(Customer customer)
        {
            customerContext.Update(customer);
        }

        public Customer Put(Customer customer)
        {
            return customerContext.Create(customer);
        }

        public void Delete(int id)
        {
            customerContext.Delete(id);
        }


    }
}
