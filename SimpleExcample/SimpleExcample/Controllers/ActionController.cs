﻿using SimpleExcample.Models;
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
        CustomerContext customerContext = new CustomerContext();
        
        public List<Customer> Get()
        { 
            return customerContext.GetUsers();
        }

        public List<Customer> Get(int id)
        {
            List<Customer> customerToList = new List<Customer>();
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
