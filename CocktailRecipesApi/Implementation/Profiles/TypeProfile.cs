using Application.DataTransfer;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles
{
    public class TypeProfile : Profile
    {
        public TypeProfile()
        {
            CreateMap<Domain.Type, TypeDto>();
            CreateMap<TypeDto, Domain.Type>();
        }
    }
}
