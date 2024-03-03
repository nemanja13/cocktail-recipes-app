using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class PagedResponse<T> where T : class
    {
        public int? TotalCount { get; set; }
        public int? CurrentPage { get; set; }
        public int? LastPage => TotalCount.HasValue && ItemsPerPage.HasValue ? (int)Math.Ceiling((float)TotalCount.Value / ItemsPerPage.Value) : null;
        public int? ItemsPerPage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
