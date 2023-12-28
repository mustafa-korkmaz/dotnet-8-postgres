
using Application.Dto.Order;
using AutoMapper;
using Infrastructure.UnitOfWork;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Application.Constants;
using Application.Exceptions;

namespace Application.Services.Order
{
    public class OrderService : ServiceBase<OrderRepository, Domain.Aggregates.Order.Order, OrderDto, Guid>, IOrderService
    {
        public OrderService(IUnitOfWork uow, ILogger<OrderService> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
        }

        public override async Task UpdateAsync(OrderDto dto)
        {
            var entity = await Repository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                throw new ValidationException(ErrorMessages.RecordNotFound);
            }

            using (var transaction = Uow.BeginTransaction())
            {
                Repository.RemoveItems(entity.Items);
                await Uow.SaveAsync();

                entity = Mapper.Map<OrderDto, Domain.Aggregates.Order.Order>(dto);

                await Repository.AddItemsAsync(entity.Items);
                await Uow.SaveAsync();

                await transaction.CommitAsync();
            }
        }
    }
}