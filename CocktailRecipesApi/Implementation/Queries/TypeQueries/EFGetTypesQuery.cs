using Application.DataTransfer;
using Application.Queries.TypeQueries;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries.TypeQueries
{
    public class EFGetTypesQuery : IGetTypesQuery
    {
        private readonly CocktailRecipesContext _context;
        private readonly IMapper _mapper;

        public EFGetTypesQuery(CocktailRecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 14;

        public string Name => "Searching Types using Entity Framework";

        public PagedResponse<TypeDto> Execute(SearchTypeDto search)
        {
            var query = _context.Types.AsQueryable();

            return query.GetPagedResponse<Domain.Type, TypeDto>(search, _mapper);
        }
    }
}

