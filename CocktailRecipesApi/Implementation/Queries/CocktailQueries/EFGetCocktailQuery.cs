using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.CocktailQueries;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries.CocktailQueries
{
    public class EFGetCocktailQuery : IGetCocktailQuery
    {
        private readonly CocktailRecipesContext _context;
        private readonly IMapper _mapper;

        public EFGetCocktailQuery(CocktailRecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 7;

        public string Name => "Finding a Cocktail using the Entity Framework";

        public CocktailDto Execute(int search)
        {
            var cocktail = _context.CocktailRecipes
                .Include(x => x.Type)
                .FirstOrDefault(x => x.Id == search);

            if (cocktail == null)
                throw new EntityNotFoundException(search, typeof(CocktailRecipe));

            return _mapper.Map<CocktailDto>(cocktail);
        }
    }
}