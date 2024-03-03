using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.IngredientQueries;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries.IngredientQueries
{
    public class EFGetIngredientQuery : IGetIngredientQuery
    {
        private readonly CocktailRecipesContext _context;
        private readonly IMapper _mapper;

        public EFGetIngredientQuery(CocktailRecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 9;

        public string Name => "Finding a Ingredient using the Entity Framework";

        public IngredientDto Execute(int search)
        {
            var ingredient = _context.Ingredients
                .FirstOrDefault(x => x.Id == search);

            if (ingredient == null)
                throw new EntityNotFoundException(search, typeof(Ingredient));

            return _mapper.Map<IngredientDto>(ingredient);
        }
    }
}
