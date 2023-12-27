using Application.Dto;
using Application.Dto.Identity;
using Application.Dto.Order;
using Application.Dto.Product;
using AutoMapper;
using Domain.Aggregates;
using Domain.Aggregates.Identity;
using Domain.Aggregates.Order;
using Domain.Aggregates.Product;
using Infrastructure.Services;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ConvertUsing(src => new User(Guid.NewGuid(), src.Username.GetNormalized(), src.Email.GetNormalized(), src.NameSurname, src.IsEmailConfirmed, src.PasswordHash, DateTimeOffset.UtcNow));

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
                .ConvertUsing(src => new Product(src.Sku!, src.Name!, src.UnitPrice, src.StockQuantity, DateTimeOffset.UtcNow));

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>()
                .ConvertUsing((src, _) =>
                {
                    var now = DateTimeOffset.UtcNow;
                    var order = new Order(Guid.NewGuid(), src.UserId, now);

                    foreach (var item in src.Items)
                    {
                        order.AddItem(Guid.NewGuid(), item.ProductId, item.UnitPrice, item.Quantity, now);
                    }

                    return order;
                });

            CreateMap<ListDtoRequest, ListEntityRequest>();
            CreateMap(typeof(ListEntityResponse<>), typeof(ListDtoResponse<>));
        }
    }
}