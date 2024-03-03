using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class UpdateIngredientValidator : AbstractValidator<IngredientDto>
    {
        public UpdateIngredientValidator(CocktailRecipesContext context)
        {
            RuleFor(r => r.Name).NotEmpty()
                .WithMessage("Ingredient {PropertyName} must not be empty.").
                DependentRules(() =>
                {
                    RuleFor(r => r.Name)
                    .Must((dto, name) => !context.Ingredients.Any(c => c.Name == name && c.Id != dto.Id))
                    .WithMessage(dto => $"Ingredient with name {dto.Name} already exists in database.");
                });
        }
    }
}
