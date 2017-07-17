using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleExample.Models
{
    public class QueryHandler
    {
        public string SQL { get; internal set; }
        public int MaxInPage { get; internal set; }
        public int CurrentPage { get; internal set; }

        public QueryHandler(string sql, int maxInPage, int currentPage)
        {
            this.SQL = sql;
            this.MaxInPage = maxInPage <= 0 ? 1 : maxInPage;
            this.CurrentPage = currentPage;
        }
    }

    public class PaginationHandler<T>
    {
        public IEnumerable<T> Customers { get; internal set; }
        public int[] TotalPages { get; }

        public PaginationHandler(IEnumerable<T> customers,int customersCount, int maxInPage)
        {
            this.Customers = customers;
            maxInPage = maxInPage <= 0 ? 1 : maxInPage;
            var countPages = (int)Math.Ceiling((double)customersCount / (double)maxInPage);
            TotalPages = new int[countPages];
            for (int i = 1; i <= countPages; i++) TotalPages[i-1] = i;
        }
    }
}