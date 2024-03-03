using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CocktailRecipe : Entity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Instructions { get; set; }
        public int TypeId { get; set; }
        public int MeasureId { get; set; }

        public virtual Type Type { get; set; }
        public virtual Measure Measure { get; set; }
        public virtual ICollection<CocktailRecipeIngredient> CocktailRecipeIngredients { get; set; } = new HashSet<CocktailRecipeIngredient>();
    }
}
