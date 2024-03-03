using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CocktailRecipeIngredient
    {
        public int CocktailRecipeId { get; set; }
        public int IngredientId { get; set; }

        public virtual CocktailRecipe CocktailRecipe { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
