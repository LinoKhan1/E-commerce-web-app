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
            // Mapping for Product and ProductDTO
            CreateMap<Product, ProductDTO>().ReverseMap();

            // Mapping for Category and CategoryDTO
            CreateMap<Category, CategoryDTO>().ReverseMap();

            // Mapping for CartItem and CartItemDTO with custom mappings
            CreateMap<CartItem, CartItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));

            // Mapping for RegisterUserDTO to User
            CreateMap<RegisterUserDTO, User>();

            // Mapping for User to UserResponseDTO
            CreateMap<User, UserResponseDTO>()
                .ForMember(dest => dest.Token, opt => opt.Ignore()); // Token will be set manually after mapping
        }
    }
}
