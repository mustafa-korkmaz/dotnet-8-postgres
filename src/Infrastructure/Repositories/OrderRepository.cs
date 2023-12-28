using Domain.Aggregates;
using Domain.Aggregates.Order;
using Infrastructure.Persistence.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order, Guid>, IOrderRepository
    {
        public OrderRepository(PostgresDbContext context) : base(context)
        {
        }

        public override async Task<Order?> GetByIdAsync(object id)
        {
            var orderId = (Guid)id;

            return await Entities.Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public override async Task<ListEntityResponse<Order>> ListAsync(ListEntityRequest request)
        {
            var result = new ListEntityResponse<Order>();

            var query = Entities.AsQueryable();

            if (request.IncludeRecordsTotal)
            {
                result.RecordsTotal = await query.CountAsync();
            }

            query = query
                .Include(o => o.Items)
                .OrderByDescending(p => p.CreatedAt);

            result.Items = await query
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToListAsync();

            return result;
        }

        public void RemoveItems(IEnumerable<OrderItem> items)
        {
            var orderItems = GetEntities<OrderItem, Guid>();

            orderItems.RemoveRange(items);
        }

        public Task AddItemsAsync(IEnumerable<OrderItem> items)
        {
            var orderItems = GetEntities<OrderItem, Guid>();

            return orderItems.AddRangeAsync(items);
        }
    }
}
