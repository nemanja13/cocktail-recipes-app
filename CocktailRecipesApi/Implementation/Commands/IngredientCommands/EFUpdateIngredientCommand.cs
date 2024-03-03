using Application.Commands.IngredientCommands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.IngredientCommands
{
    public class EFUpdateIngredientCommand : IUpdateIngredientCommand
    {

        private readonly CocktailRecipesContext _context;
        private readonly UpdateIngredientValidator _validator;
        private readonly IMapper _mapper;

        public EFUpdateIngredientCommand(CocktailRecipesContext context, UpdateIngredientValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 2;

        public string Name => "Updating Ingredient using Entity Framework";

        public void Execute(IngredientDto request)
        {
            var ingredient = _context.Ingredients.FirstOrDefault(c => c.Id == request.Id);

            if (ingredient == null)
            {
                throw new EntityNotFoundException(request.Id.Value, typeof(Ingredient));
            }

            _validator.ValidateAndThrow(request);

            _mapper.Map(request, ingredient);

            _context.SaveChanges();
        }
    }
}
