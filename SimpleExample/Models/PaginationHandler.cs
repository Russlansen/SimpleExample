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

        public PaginationHandler(IEnumerable<T> customers, int customersTotalCount, int maxCustomerPerPage, 
                                                           int totalPagesMax, int currentPage)
        {
            this.Customers = customers;
            maxCustomerPerPage = maxCustomerPerPage <= 0 ? 1 : maxCustomerPerPage;
            totalPagesMax = totalPagesMax <= 0 ? 1 : totalPagesMax;
            CountPages = (int)Math.Ceiling((double)customersTotalCount / (double)maxCustomerPerPage);
            int pagerMiddle = (int)Math.Ceiling((double)totalPagesMax / 2);

            if (totalPagesMax > CountPages) {
                TotalPages = new int[CountPages];
                for (int i = 1; i <= CountPages; i++) TotalPages[i - 1] = i;
            }
            else
            {
                int startPage = currentPage - (pagerMiddle - 1);
                int endPage = totalPagesMax % 2 == 0 ? currentPage + (pagerMiddle) : currentPage + (pagerMiddle - 1);
                TotalPages = new int[totalPagesMax];

                if (currentPage < pagerMiddle)
                {
                    for (int i = 1; i <= totalPagesMax; i++) TotalPages[i - 1] = i;
                }     
                else if (currentPage >= pagerMiddle && currentPage <= CountPages - pagerMiddle)
                {
                    for (int i = startPage, j = 0; i <= endPage; i++, j++)
                        TotalPages[j] = i;
                }
                else
                {
                    for (int i = CountPages - (totalPagesMax - 1), j = 0; i <= CountPages; i++, j++)
                        TotalPages[j] = i;
                }       
            }
        }
    }
}