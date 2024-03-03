using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TypeQueries
{
    public interface IGetTypesQuery : IQuery<SearchTypeDto, PagedResponse<TypeDto>>
    {
    }
}
