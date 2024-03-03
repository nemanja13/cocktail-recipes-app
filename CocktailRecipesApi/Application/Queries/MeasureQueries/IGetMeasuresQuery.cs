using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.MeasureQueries
{
    public interface IGetMeasuresQuery : IQuery<SearchMeasureDto, PagedResponse<MeasureDto>>
    {
    }
}
