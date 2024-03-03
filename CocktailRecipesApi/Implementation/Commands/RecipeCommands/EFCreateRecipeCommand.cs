using Application.Commands.RecipeCommands;
using Application.DataTransfer;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.RecipeCommands
{
    public class EFCreateRecipeCommand : ICreateRecipeCommand
    {
        private readonly CocktailRecipesContext _context;
        private readonly CreateRecipeValidator _validator;
        private readonly IMapper _mapper;

        public EFCreateRecipeCommand(CocktailRecipesContext context, CreateRecipeValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Creating New Cocktail recipe using Entity Framework";

        public void Execute(RecipeDto request)
        {
            _validator.ValidateAndThrow(request);

            var cocktailRecipe = _mapper.Map<CocktailRecipe>(request);

            foreach (var ingredientId in request.IngredientIds)
            {
                _context.CocktailRecipeIngredients.Add(new CocktailRecipeIngredient
                {
                    IngredientId = ingredientId,
                    CocktailRecipe = cocktailRecipe
                });
            }

            _context.CocktailRecipes.Add(cocktailRecipe);
            _context.SaveChanges();
        }
    }
}
