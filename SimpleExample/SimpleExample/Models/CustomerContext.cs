using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SimpleExample.Models
{
    public class CustomerContext
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<Customer> GetUsers()
        {
            List<Customer> users = new List<Customer>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                users = db.Query<Customer>("SELECT * FROM Customers").ToList();
            }
            return users;
        }

        public Customer Get(int id)
        {
            
            Customer customer = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                customer = db.Query<Customer>("SELECT * FROM Customers WHERE Id = @id", new { id }).FirstOrDefault();
            }
            if (customer != null)
                return customer;
            else return new Customer(0, "Customer not found", 0);
        }

        public Customer Create(Customer customer)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = String.Format("INSERT INTO Customers (Name, Age) VALUES('{0}', {1})", customer.Name, customer.Age);
                int? customerId = db.Query<int>(sqlQuery).FirstOrDefault();
                if(customerId != null)
                    customer.Id = (int)customerId;
            }
            return customer;
        }

        public void Update(Customer customer)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Customers SET Name = @Name, Age = @Age WHERE Id = @Id";
                db.Execute(sqlQuery, customer);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Customers WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}