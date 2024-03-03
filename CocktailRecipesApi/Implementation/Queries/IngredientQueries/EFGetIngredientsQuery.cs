using Application.DataTransfer;
using Application.Queries.IngredientQueries;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries.IngredientQueries
{
    public class EFGetIngredientsQuery : IGetIngredientsQuery
    {
        private readonly CocktailRecipesContext _context;
        private readonly IMapper _mapper;

        public EFGetIngredientsQuery(CocktailRecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 10;

        public string Name => "Searching Ingredients using Entity Framework";

        public PagedResponse<IngredientDto> Execute(SearchIngredientDto search)
        {
            var query = _context.Ingredients.AsQueryable();

            return query.GetPagedResponse<Ingredient, IngredientDto>(search, _mapper);
        }
    }
}

