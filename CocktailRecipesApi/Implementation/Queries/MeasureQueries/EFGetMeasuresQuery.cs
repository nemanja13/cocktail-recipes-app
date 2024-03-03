using Application.DataTransfer;
using Application.Queries.MeasureQueries;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries.MeasureQueries
{
    public class EFGetMeasuresQuery : IGetMeasuresQuery
    {
        private readonly CocktailRecipesContext _context;
        private readonly IMapper _mapper;

        public EFGetMeasuresQuery(CocktailRecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 11;

        public string Name => "Searching Measures using Entity Framework";

        public PagedResponse<MeasureDto> Execute(SearchMeasureDto search)
        {
            var query = _context.Measures.AsQueryable();

            return query.GetPagedResponse<Measure, MeasureDto>(search, _mapper);
        }
    }
}
