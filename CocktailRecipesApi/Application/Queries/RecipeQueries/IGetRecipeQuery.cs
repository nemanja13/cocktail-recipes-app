﻿using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.RecipeQueries
{
    public interface IGetRecipeQuery : IQuery<int, RecipeDto>
    {
    }
}
