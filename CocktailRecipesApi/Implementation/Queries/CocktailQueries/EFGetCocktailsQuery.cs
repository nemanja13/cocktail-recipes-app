using Application.DataTransfer;
using Application.Queries.CocktailQueries;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Implementation.Queries.CocktailQueries
{
    public class EFGetCocktailsQuery : IGetCocktailsQuery
    {
        private readonly CocktailRecipesContext _context;
        private readonly IMapper _mapper;

        public EFGetCocktailsQuery(CocktailRecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 8;

        public string Name => "Searching Cocktails using Entity Framework";

        public PagedResponse<CocktailDto> Execute(SearchCocktailDto search)
        {
            var query = _context.CocktailRecipes
                .Include(x => x.Type)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));

            if (search.TypeId.HasValue)
                query = query.Where(x => x.TypeId == search.TypeId);

            if (search.OrderBy.HasValue)
            {
                switch (search.OrderBy.Value)
                {
                    case OrderBy.NameAsc:
                        query = query.OrderBy(x => x.Name);
                        break;
                    case OrderBy.NameDsc:
                        query = query.OrderByDescending(x => x.Name);
                        break;
                    case OrderBy.CreationAsc:
                        query = query.OrderBy(x => x.CreationDate);
                        break;
                    case OrderBy.CreationDsc:
                        query = query.OrderByDescending(x => x.CreationDate);
                        break;
                }
            }

            return query.GetPagedResponse<CocktailRecipe, CocktailDto>(search, _mapper);
        }
    }
}
