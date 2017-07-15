using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleExcample.Models
{
    public class PaginationHandler
    {
        /// <summary>
        /// Return list of customers for pagination
        /// </summary>
        /// <param name="allCustomers">Recive full list of customers</param>
        /// <param name="maxInPage">Max number of customers per page</param>
        /// <param name="currentPage">Current page</param>
        /// <returns></returns>
        internal static List<Customer> GetPagination(List<Customer> allCustomers, int maxInPage, int currentPage)
        {
            maxInPage = maxInPage <= 0 ? 1 : maxInPage; 
            int totalPages = (int)Math.Ceiling((double)allCustomers.Count / (double)maxInPage);
            //Calculating an index of first element on page 
            int firstElement = maxInPage * (currentPage - 1); 
            List<Customer> customerForPagination = new List<Customer>();
            try
            {
                for (int i = 0; i < maxInPage; i++)
                {
                    customerForPagination.Add(allCustomers[firstElement + i]);
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                return customerForPagination;
            }

            return customerForPagination;

        }
    }
}