using AutoMapper;
using e_commerce_app.Server.APIs.DTOs.AuthenticationDTOs;
using e_commerce_app.Server.APIs.DTOs.CartDTOs;
using e_commerce_app.Server.APIs.DTOs.CategoryDTOs;
using e_commerce_app.Server.APIs.DTOs.ProductDTOs;
using e_commerce_app.Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_app.tests.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<CartItem, CartItemDTO>()
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
            CreateMap<RegisterUserDTO, User>();
            CreateMap<User, UserResponseDTO>()
                .ForMember(dest => dest.Token, opt => opt.Ignore()); // Token will be set manually after mapping


        }
    }
}
