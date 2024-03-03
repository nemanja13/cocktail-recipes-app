using Application.Commands.RecipeCommands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.RecipeCommands
{
    public class EFDeleteRecipeCommand : IDeleteRecipeCommand
    {
        private readonly CocktailRecipesContext _context;

        public EFDeleteRecipeCommand(CocktailRecipesContext context)
        {
            _context = context;
        }

        public int Id => 6;

        public string Name => "Deleting Cocktail recipe using Entity Framework";

        public void Execute(int request)
        {
            var cocktailRecipe = _context.CocktailRecipes.Find(request);

            if (cocktailRecipe == null)
            {
                throw new EntityNotFoundException(request, typeof(CocktailRecipe));
            }

            cocktailRecipe.DeleteDate = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
