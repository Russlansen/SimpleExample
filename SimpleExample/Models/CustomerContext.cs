using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace SimpleExample.Models
{
    public class CustomerContext
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        const string tableName = "Customers";

        public PaginationHandler<T> GetCustomersForPagination<T>(int maxCustomerPerPage, int totalPages, int currentPage,
                                                                             string orderBy, string order)
        {
            int count;
            IEnumerable<T> customers;
            maxCustomerPerPage = maxCustomerPerPage <= 0 ? 1 : maxCustomerPerPage;
            totalPages = totalPages <= 0 ? 1 : totalPages;
            var queryString = $"SELECT * FROM {tableName} ORDER BY {orderBy} {order} OFFSET " +
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
                ThrowException(HttpStatusCode.BadRequest, new { Message = "Action error" });
                return null;
            }
            return new PaginationHandler<T>(customers, count, maxCustomerPerPage, totalPages, currentPage);
        }

        public Customer Get(int id)
        {       
            Customer customer = null;
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    var sqlQuery = $"SELECT * FROM {tableName} WHERE Id = @Id";
                    customer = db.Query<Customer>(sqlQuery, new { Id = id }).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                ThrowException(HttpStatusCode.BadRequest, new { Message = "Action error" });
            }
            if (customer == null) ThrowException(HttpStatusCode.NotFound, new { Message = $"Customer with ID = {id} not found" });
            return customer;
        }

        public void Create(Customer customer)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    var sqlQuery = $"INSERT INTO {tableName} (Name, Age) VALUES(@Name, @Age)";
                    db.Query<int>(sqlQuery, new { Name = customer.Name, Age = customer.Age });
                }
            }
            catch (Exception)
            {
                ThrowException(HttpStatusCode.BadRequest, new { Message = "Action error" });
            }
        }

        public void Update(Customer customer)
        {
            int rowsAffected = 0;
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    var sqlQuery = $"UPDATE {tableName} SET Name = @Name, Age = @Age WHERE Id = @Id";
                    rowsAffected = db.Execute(sqlQuery, new { Name = customer.Name, Age = customer.Age, Id = customer.Id });     
                }
            }
            catch (Exception)
            {
                ThrowException(HttpStatusCode.BadRequest, new { Message = "Action error" });
            }
            if (rowsAffected == 0) ThrowException(HttpStatusCode.NotFound,
                                   new { Message = $"Updating error. Customer with ID = {customer.Id} not found" });
        }

        public void Delete(int id)
        {
            int rowsAffected = 0;
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    var sqlQuery = $"DELETE FROM {tableName} WHERE Id = @Id";
                    rowsAffected = db.Execute(sqlQuery, new { Id = id });   
                }
            }
            catch (Exception)
            {
                ThrowException(HttpStatusCode.BadRequest, new { Message = "Action error"});
            }
            if (rowsAffected == 0) ThrowException(HttpStatusCode.NotFound,
                                              new { Message = $"Deleting error. Customer with ID = {id} not found" });
        }

        private void ThrowException(HttpStatusCode statusCode, object exeptionContent)
        {
            var jsonExeption = JsonConvert.SerializeObject(exeptionContent);
            var response = new HttpResponseMessage(statusCode);
            response.Content = new StringContent(jsonExeption);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            throw new HttpResponseException(response);
        }
    }
}