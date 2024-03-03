using Application.DataTransfer;
using Application.Queries.RecipeQueries;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Implementation.Queries.RecipeQueries
{
    public class EFGetRecipesQuery : IGetRecipesQuery
    {
        private readonly CocktailRecipesContext _context;
        private readonly IMapper _mapper;

        public EFGetRecipesQuery(CocktailRecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 13;

        public string Name => "Searching Recipes using Entity Framework";

        public PagedResponse<RecipeDto> Execute(SearchRecipeDto search)
        {
            var query = _context.CocktailRecipes.AsQueryable();

            return query.GetPagedResponse<CocktailRecipe, RecipeDto>(search, _mapper);
        }
    }
}
