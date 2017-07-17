using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleExample.Models
{
    public class PaginationHandler
    {
        CustomerContext customerContext = new CustomerContext();

        public int[] TotalPages{ get; internal set; }
        public List<Customer> Customers { get; internal set; }
        public readonly List<Customer> customersForPagination = new List<Customer>();

        /// <summary>
        /// Set a property for pagination handler
        /// </summary>
        /// <param name="maxInPage">Max number of customers per page</param>
        /// <param name="currentPage">Current page</param>
        /// <returns></returns>
        public void SetPagination(int maxInPage, int currentPage)
        {
            Customers = customerContext.GetUsers();    
            maxInPage = maxInPage <= 0 ? 1 : maxInPage;
            int countPages = (int)Math.Ceiling((double)Customers.Count / (double)maxInPage);

            //TotalPages property contain an array to use in ng-repeat
            TotalPages = new int[countPages];
            for (int i = 1; i <= countPages; i++) TotalPages[i-1] = i;

            //Calculating an index of first element on page 
            int firstElement = maxInPage * (currentPage - 1); 
            //CustomerForPagination = new List<Customer>();
            
            for (int i = 0; i < maxInPage; i++)
            {
                try
                {
                    customersForPagination.Add(Customers[firstElement + i]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
        }
            
    }
}