using SimpleExcample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleExcample.Controllers
{
    public class ActionController : ApiController
    {
        public string Get()
        {
            return "Test data from GET method";
        }

        public string Post(string id)
        {
            return id;
        }

        public IEnumerable<Customer> Post(IEnumerable<Customer> customer)
        {
            return customer;
        }

        [HttpPost]
        public string GetString(string simpleText)
        {
            return simpleText;
        }
    }
}
