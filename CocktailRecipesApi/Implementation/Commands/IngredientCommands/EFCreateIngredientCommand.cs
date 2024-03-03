using Application.Commands.IngredientCommands;
using Application.DataTransfer;
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
    public class EFCreateIngredientCommand : ICreateIngredientCommand
    {
        private readonly CocktailRecipesContext _context;
        private readonly CreateIngredientValidator _validator;
        private readonly IMapper _mapper;

        public EFCreateIngredientCommand(CocktailRecipesContext context, CreateIngredientValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Creating New Ingredient using Entity Framework";

        public void Execute(IngredientDto request)
        {
            _validator.ValidateAndThrow(request);

            var ingredient = _mapper.Map<Ingredient>(request);

            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();
        }
    }
}