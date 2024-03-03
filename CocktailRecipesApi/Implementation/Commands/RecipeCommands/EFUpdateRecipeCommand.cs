using Application.Commands.RecipeCommands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.RecipeCommands
{
    public class EFUpdateRecipeCommand : IUpdateRecipeCommand
    {

        private readonly CocktailRecipesContext _context;
        private readonly UpdateRecipeValidator _validator;
        private readonly IMapper _mapper;

        public EFUpdateRecipeCommand(CocktailRecipesContext context, UpdateRecipeValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 5;

        public string Name => "Updating Cocktail recipe using Entity Framework";

        public void Execute(RecipeDto request)
        {
            var cocktailRecipe = _context.CocktailRecipes.Include(c => c.CocktailRecipeIngredients).FirstOrDefault(c => c.Id == request.Id);

            if (cocktailRecipe == null)
            {
                throw new EntityNotFoundException(request.Id.Value, typeof(CocktailRecipe));
            }

            _validator.ValidateAndThrow(request);

            _mapper.Map(request, cocktailRecipe);


            cocktailRecipe.CocktailRecipeIngredients.Where(ci => !request.IngredientIds.Contains(ci.IngredientId)).ToList().ForEach(ci => cocktailRecipe.CocktailRecipeIngredients.Remove(ci));
            var existingIngredientIds = cocktailRecipe.CocktailRecipeIngredients.Select(ci => ci.IngredientId);
            _context.Ingredients.Where(i => request.IngredientIds.Except(existingIngredientIds).Contains(i.Id)).ToList().ForEach(ingredient => _context.CocktailRecipeIngredients.Add(new CocktailRecipeIngredient
            {
                Ingredient = ingredient,
                CocktailRecipe = cocktailRecipe
            }));

            _context.SaveChanges();
        }
    }
}
