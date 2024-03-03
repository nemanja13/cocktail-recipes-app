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
    public class UpdateRecipeValidator : AbstractValidator<RecipeDto>
    {
        public UpdateRecipeValidator(CocktailRecipesContext context)
        {
            RuleFor(r => r.Name).NotEmpty()
                .WithMessage("Recipe {PropertyName} must not be empty.").
                DependentRules(() =>
                {
                    RuleFor(r => r.Name)
                    .Must((dto, name) => !context.CocktailRecipes.Any(c => c.Name == name && c.Id != dto.Id))
                    .WithMessage(dto => $"Cocktail with name {dto.Name} already exists in database.");
                });

            RuleFor(r => r.Instructions).NotEmpty()
                .WithMessage("Recipe {PropertyName} must not be empty.");

            RuleFor(r => r.TypeId).NotEmpty()
                .WithMessage("You must enter a type").
                DependentRules(() =>
                {
                    RuleFor(r => r.TypeId)
                    .Must(id => context.Types.Any(c => c.Id == id))
                    .WithMessage((dto, id) => $"Type with an id of {id} does not exists in database.");
                });

            RuleFor(r => r.MeasureId).NotEmpty()
                .WithMessage("You must enter a measure").
                DependentRules(() =>
                {
                    RuleFor(r => r.MeasureId)
                    .Must(id => context.Measures.Any(m => m.Id == id))
                    .WithMessage((dto, id) => $"Measure with an id of {id} does not exists in database.");
                });

            RuleFor(r => r.IngredientIds).NotEmpty()
                .WithMessage("You must assign a ingredient to the cocktail recipe")
                .DependentRules(() => {
                    RuleFor(r => r.IngredientIds)
                    .Must(ids => ids.Distinct().Count() == ids.Count())
                    .WithMessage("You have entered double values ​​for the ingredient");
                    RuleForEach(r => r.IngredientIds).Must(id => context.Ingredients.Any(i => i.Id == id))
                    .WithMessage((dto, id) => $"Ingredient with an id of {id} does not exists in database.");
                });

        }
    }
}
