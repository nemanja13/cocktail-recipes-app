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
    public class MeasureProfile : Profile
    {
        public MeasureProfile()
        {
            CreateMap<Measure, MeasureDto>();
            CreateMap<MeasureDto, Measure>();
        }
    }
}
