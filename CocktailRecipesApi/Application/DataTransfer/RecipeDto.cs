using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public class RecipeDto : DtoBase
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Instructions { get; set; }
        public int? TypeId { get; set; }
        public string? Type { get; set; }
        public int? MeasureId { get; set; }
        public string? Measure { get; set; }
        public IEnumerable<int> IngredientIds { get; set; } = new List<int>();
        public IEnumerable<string> Ingredients { get; set; } = new List<string>();
    }
}
