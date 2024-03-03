using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.CocktailQueries
{
    public interface IGetCocktailsQuery : IQuery<SearchCocktailDto, PagedResponse<CocktailDto>>
    {
    }
}
