using AutoMapper;
using FoodStore.DTO;
using FoodStore.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FoodStore.Mapping
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() { 
        
            CreateMap<Food, FoodDTO>().ReverseMap();
            CreateMap<FoodCategory, FoodCategoryDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();

        }

    }
}
