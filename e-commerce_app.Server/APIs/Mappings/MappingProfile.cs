using AutoMapper;
using e_commerce_app.Server.APIs.DTOs.AuthenticationDTOs;
using e_commerce_app.Server.APIs.DTOs.CartDTOs;
using e_commerce_app.Server.APIs.DTOs.CategoryDTOs;
using e_commerce_app.Server.APIs.DTOs.ProductDTOs;
using e_commerce_app.Server.Core.Entities;

namespace e_commerce_app.Server.APIs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
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
