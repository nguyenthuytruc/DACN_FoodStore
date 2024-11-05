using AutoMapper;
using FoodStore.DTO;
using FoodStore.Models;

namespace FoodStore.Mapping
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() { 
        
            CreateMap<Food, FoodDTO>().ReverseMap();
        }

    }
}
