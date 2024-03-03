using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Ingredient : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<CocktailRecipeIngredient> CocktailRecipeIngredients { get; set; } = new HashSet<CocktailRecipeIngredient>();
    }
}
