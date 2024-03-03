using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Searches
{
    public abstract class PagedSearch
    {
        public string? Keyword { get; set; }
        public int? PerPage { get; set; }
        public int? Page { get; set; }
    }
}
