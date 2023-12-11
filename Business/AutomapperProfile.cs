using System.Reflection.PortableExecutable;
using AutoMapper;
using Business.Models;

using DAL.Entities;

namespace Business
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Pizza, PizzaModel>()
                .ReverseMap();

            CreateMap<Manufacturer, ManufacturerModel>()
                .ForMember(p => p.PizzasIds, c => c.MapFrom(m => m.Pizzas.Select(x => x.Id)))
                .ReverseMap();
        }
    }
}