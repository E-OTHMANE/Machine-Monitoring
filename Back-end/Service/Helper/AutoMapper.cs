using AutoMapper;
using Models;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helper
{
    public class AutoMapper : Profile
    {
        
        public AutoMapper()
        {
            CreateMap<Machine,MachineProductionDTO>()
                .ForMember(element=> element.TotalProduction,
                    func=>func.MapFrom(function=>function.MachineProductions.Sum(arg=>arg.TotalProduction)))
                .ReverseMap();
            CreateMap<Machine,MachineDTO>().ReverseMap();
        }


    }
}
