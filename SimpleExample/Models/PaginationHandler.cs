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

        public PaginationHandler(PaginationConfig<T> config)
        {
            Customers = config.Customers;
            config.MaxCustomerPerPage = config.MaxCustomerPerPage <= 0 ? 1 : config.MaxCustomerPerPage;
            config.TotalPagesMax = config.TotalPagesMax <= 0 ? 1 : config.TotalPagesMax;
            CountPages = (int)Math.Ceiling((double)config.CustomersTotalCount / (double)config.MaxCustomerPerPage);
            int pagerMiddle = (int)Math.Ceiling((double)config.TotalPagesMax / 2);

            if (config.TotalPagesMax > CountPages) {
                TotalPages = new int[CountPages];
                for (int i = 1; i <= CountPages; i++) TotalPages[i - 1] = i;
            }
            else
            {
                int startPage = config.CurrentPage - (pagerMiddle - 1);
                int endPage = config.TotalPagesMax % 2 == 0 ? config.CurrentPage + (pagerMiddle) : config.CurrentPage + (pagerMiddle - 1);
                TotalPages = new int[config.TotalPagesMax];

                if (config.CurrentPage < pagerMiddle)
                {
                    for (int i = 1; i <= config.TotalPagesMax; i++) TotalPages[i - 1] = i;
                }     
                else if (config.CurrentPage >= pagerMiddle && config.CurrentPage <= CountPages - pagerMiddle)
                {
                    for (int i = startPage, j = 0; i <= endPage; i++, j++)
                        TotalPages[j] = i;
                }
                else
                {
                    for (int i = CountPages - (config.TotalPagesMax - 1), j = 0; i <= CountPages; i++, j++)
                        TotalPages[j] = i;
                }       
            }
        }
    }
}