using Application.Commands.IngredientCommands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.IngredientCommands
{
    public class EFDeleteIngredientCommand : IDeleteIngredientCommand
    {
        private readonly CocktailRecipesContext _context;

        public EFDeleteIngredientCommand(CocktailRecipesContext context)
        {
            _context = context;
        }

        public int Id => 3;

        public string Name => "Deleting Ingredient using Entity Framework";

        public void Execute(int request)
        {
            var ingredient = _context.Ingredients.Find(request);

            if (ingredient == null)
            {
                throw new EntityNotFoundException(request, typeof(Ingredient));
            }

            ingredient.DeleteDate = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
