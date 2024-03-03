using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public class CocktailDto : DtoBase
    {
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public string Type { get; set; }
    }
}
