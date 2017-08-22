using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleExample.Models
{
    public class PaginationConfig<T>
    {
        public IEnumerable<T> Customers { get; set; }
        public int CustomersTotalCount { get; set; }
        public int MaxCustomerPerPage { get; set; }
        public int TotalPagesMax { get; set; }
        public int CurrentPage { get; set; }
        public string OrderBy { get; set; }
        public string Order { get; set; }
    }
}