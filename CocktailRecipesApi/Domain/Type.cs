using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Type : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<CocktailRecipe> CocktailRecipes { get; set; } = new HashSet<CocktailRecipe>();
    }
}
