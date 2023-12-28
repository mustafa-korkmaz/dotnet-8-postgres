using Application.Dto;
using Application.Dto.Identity;
using Application.Dto.Order;
using Application.Dto.Product;
using AutoMapper;
using Domain.Aggregates;
using Domain.Aggregates.Identity;
using Domain.Aggregates.Order;
using Domain.Aggregates.Product;

namespace Application
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ConvertUsing(src => new User(src.Id, src.Username, src.Email, src.NameSurname, src.IsEmailConfirmed, src.PasswordHash, src.CreatedAt));

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
                .ConvertUsing(src => new Product(src.Id, src.Sku, src.Name, src.UnitPrice, src.StockQuantity, src.CreatedAt));

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>()
                .ConvertUsing((src, _) =>
                {
                    var order = new Order(src.Id, src.UserId, src.CreatedAt);

                    foreach (var item in src.Items)
                    {
                        order.AddItem(Guid.NewGuid(), item.ProductId, item.UnitPrice, item.Quantity, src.CreatedAt);
                    }

                    return order;
                });

            CreateMap<ListDtoRequest, ListEntityRequest>();
            CreateMap(typeof(ListEntityResponse<>), typeof(ListDtoResponse<>));
        }
    }
}