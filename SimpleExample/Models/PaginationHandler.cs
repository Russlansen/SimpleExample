using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SimpleExample.Models
{
    public class PaginationHandler<T>
    {
        public IEnumerable<T> Customers { get; internal set; }
        public int CountPages { get; }
        public int[] TotalPages { get; }
        private int pagerMax = 9;

        public PaginationHandler(IEnumerable<T> customers, int customersTotalCount, int maxCustomerPerPage, int currentPage)
        {
            this.Customers = customers;
            maxCustomerPerPage = maxCustomerPerPage <= 0 ? 1 : maxCustomerPerPage;
            CountPages = (int)Math.Ceiling((double)customersTotalCount / (double)maxCustomerPerPage);
            int pagerMiddle = (int)Math.Ceiling((double)pagerMax / 2);

            if (pagerMax > CountPages) {
                TotalPages = new int[CountPages];
                for (int i = 1; i <= CountPages; i++) TotalPages[i - 1] = i;
            }
            else
            {
                TotalPages = new int[pagerMax];
                if (currentPage < pagerMiddle)
                    for (int i = 1; i <= pagerMax; i++) TotalPages[i - 1] = i;
                else if (currentPage >= pagerMiddle && currentPage <= CountPages - pagerMiddle)
                    for (int i = currentPage - (pagerMiddle-1), j = 0; i <= currentPage + (pagerMiddle-1); i++, j++)
                        TotalPages[j] = i;
                else
                    for (int i = CountPages - (pagerMax - 1), j = 0; i <= CountPages; i++, j++)
                        TotalPages[j] = i;
            }
            
        }
    }
}