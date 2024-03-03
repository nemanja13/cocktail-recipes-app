using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.IngredientQueries
{
    public interface IGetIngredientQuery : IQuery<int, IngredientDto>
    {
    }
}
