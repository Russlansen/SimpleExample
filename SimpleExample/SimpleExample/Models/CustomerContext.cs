using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace SimpleExample.Models
{
    public class CustomerContext
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        const string tableName = "Customers2";

        public List<Customer> GetUsers()
        {
            var customers = new List<Customer>();
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    customers = db.Query<Customer>($"SELECT * FROM {tableName}").ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return customers;
        }

        public PaginationHandler<T> GetCustomersForPagination<T>(int maxCustomerPerPage, int currentPage)
        {
            int count;
            IEnumerable<T> customers;
            maxCustomerPerPage = maxCustomerPerPage <= 0 ? 1 : maxCustomerPerPage;
            var queryString = $"SELECT * FROM {tableName} ORDER BY id OFFSET " +
                              $"{maxCustomerPerPage} * ({currentPage} - 1)" +
                              $" ROWS FETCH NEXT {maxCustomerPerPage} ROWS ONLY";                    
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    customers = db.Query<T>(queryString);
                    count = db.Query<int>($"SELECT COUNT (*) FROM {tableName}").FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return new PaginationHandler<T>(customers, count, maxCustomerPerPage);
        }

        public Customer Get(int id)
        {       
            Customer customer = null;
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    customer = db.Query<Customer>($"SELECT * FROM {tableName} WHERE Id = { id }").FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return new Customer(0, "Customer not found", 0);
            }
            if (customer != null)
                return customer;
            else return new Customer(0, "Customer not found", 0);
        }

        public void Create(Customer customer)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    var sqlQuery = String.Format($"INSERT INTO {tableName} (Name, Age) " +
                                                 $"VALUES('{customer.Name}', {customer.Age})");

                    db.Query<int>(sqlQuery);
                }
            }
            catch (Exception)
            {

            }
        }

        public void Update(Customer customer)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    var sqlQuery = $"UPDATE {tableName} SET Name = '{customer.Name}'," +
                                   $" Age = {customer.Age} WHERE Id = {customer.Id}";
                    db.Execute(sqlQuery);
                }
            }
            catch (Exception)
            {

            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    var sqlQuery = $"DELETE FROM {tableName} WHERE Id = { id }";
                    db.Execute(sqlQuery);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}