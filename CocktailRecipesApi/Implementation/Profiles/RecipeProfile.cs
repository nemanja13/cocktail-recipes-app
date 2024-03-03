using Application.DataTransfer;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<CocktailRecipe, RecipeDto>()
                .ForMember(dto => dto.Type, opt => opt.MapFrom(cocktail => cocktail.Type.Name))
                .ForMember(dto => dto.TypeId, opt => opt.MapFrom(cocktail => cocktail.Type.Id))
                .ForMember(dto => dto.Measure, opt => opt.MapFrom(cocktail => cocktail.Measure.Name))
                .ForMember(dto => dto.MeasureId, opt => opt.MapFrom(cocktail => cocktail.Measure.Id))
                .ForMember(dto => dto.Ingredients, opt => opt.MapFrom(cocktail => cocktail.CocktailRecipeIngredients.Select(ci => ci.Ingredient.Name)))
                .ForMember(dto => dto.IngredientIds, opt => opt.MapFrom(cocktail => cocktail.CocktailRecipeIngredients.Select(ci => ci.Ingredient.Id)));

            CreateMap<RecipeDto, CocktailRecipe>();
        }
    }
}
