using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.RecipeQueries;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries.RecipeQueries
{
    public class EFGetRecipeQuery : IGetRecipeQuery
    {
        private readonly CocktailRecipesContext _context;
        private readonly IMapper _mapper;

        public EFGetRecipeQuery(CocktailRecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 12;

        public string Name => "Finding a Recipe using the Entity Framework";

        public RecipeDto Execute(int search)
        {
            var cocktail = _context.CocktailRecipes
                .Include(x => x.Type)
                .Include(x => x.Measure)
                .Include(x => x.CocktailRecipeIngredients)
                .ThenInclude(ci => ci.Ingredient)
                .FirstOrDefault(x => x.Id == search);

            if (cocktail == null)
                throw new EntityNotFoundException(search, typeof(CocktailRecipe));

            return _mapper.Map<RecipeDto>(cocktail);
        }
    }
}
