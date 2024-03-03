using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Searches
{
    public class SearchRecipeDto : PagedSearch
    {
        public int? TypeId { get; set; }
        public OrderBy? OrderBy { get; set; }
    }
}
