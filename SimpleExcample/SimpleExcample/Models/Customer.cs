using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleExcample.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Customer(int Id, string name, int age)
        {
            this.Id = Id;
            this.Name = name;
            this.Age = age;
        }
    }
}