using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleExample.Models
{
    public class PaginationHandler<T>
    {
        public IEnumerable<T> Customers { get; internal set; }
        public int[] TotalPages { get; }

        public PaginationHandler(IEnumerable<T> customers, int customersTotalCount, int maxCustomerPerPage)
        {
            this.Customers = customers;
            maxCustomerPerPage = maxCustomerPerPage <= 0 ? 1 : maxCustomerPerPage;
            var countPages = (int)Math.Ceiling((double)customersTotalCount / (double)maxCustomerPerPage);
            TotalPages = new int[countPages];
            for (int i = 1; i <= countPages; i++) TotalPages[i-1] = i;
        }
    }
}