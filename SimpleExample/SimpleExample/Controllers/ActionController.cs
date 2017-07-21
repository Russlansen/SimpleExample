using SimpleExample.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleExample.Controllers
{
    public class ActionController : ApiController
    {
        CustomerContext customerContext = new CustomerContext();

        public PaginationHandler<Customer> GetPagination(int maxCustomerPerPage, int currentPage)
        {
            return customerContext.GetCustomersForPagination<Customer>(maxCustomerPerPage, currentPage);
        }

        public List<Customer> Get(int id)
        {
            return new List<Customer> { customerContext.Get(id) };
        }

        public void Post(Customer customer)
        {
            customerContext.Update(customer);
        }

        public void Put(Customer customer)
        {
            customerContext.Create(customer);
        }

        public void Delete(int id)
        {
            customerContext.Delete(id);       
        }


    }
}
