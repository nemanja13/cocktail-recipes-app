using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.RecipeQueries
{
    public interface IGetRecipesQuery : IQuery<SearchRecipeDto, PagedResponse<RecipeDto>>
    {
    }
}
