using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Searches
{
    public class SearchCocktailDto : PagedSearch
    {
        public int? TypeId { get; set; }
        public OrderBy? OrderBy { get; set; }
    }
    public enum OrderBy
    {
        NameAsc,
        NameDsc,
        CreationAsc,
        CreationDsc
    }
}
