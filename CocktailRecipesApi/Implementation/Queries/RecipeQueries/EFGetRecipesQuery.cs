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
using Microsoft.EntityFrameworkCore;

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
            var query = _context.CocktailRecipes
                .Include(x => x.Type)
                .Include(x => x.Measure)
                .Include(x => x.CocktailRecipeIngredients)
                .ThenInclude(x => x.Ingredient)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));

            if (search.TypeId.HasValue)
                query = query.Where(x => x.TypeId == search.TypeId);

            if (search.OrderBy.HasValue)
            {
                switch (search.OrderBy.Value)
                {
                    case OrderBy.NameAsc:
                        query = query.OrderBy(x => x.Name);
                        break;
                    case OrderBy.NameDsc:
                        query = query.OrderByDescending(x => x.Name);
                        break;
                    case OrderBy.CreationAsc:
                        query = query.OrderBy(x => x.CreationDate);
                        break;
                    case OrderBy.CreationDsc:
                        query = query.OrderByDescending(x => x.CreationDate);
                        break;
                }
            }

            return query.GetPagedResponse<CocktailRecipe, RecipeDto>(search, _mapper);
        }
    }
}
