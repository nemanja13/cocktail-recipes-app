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
    public class CocktailProfile : Profile
    {
        public CocktailProfile()
        {
            CreateMap<CocktailRecipe, CocktailDto>()
                .ForMember(dto => dto.Type, opt => opt.MapFrom(cocktail => cocktail.Type.Name))
                .ForMember(dto => dto.TypeId, opt => opt.MapFrom(cocktail => cocktail.Type.Id));

            CreateMap<CocktailDto, CocktailRecipe>();
        }
    }
}
