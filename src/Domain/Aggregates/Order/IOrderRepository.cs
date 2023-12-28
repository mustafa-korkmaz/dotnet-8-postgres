namespace Domain.Aggregates.Order
{
    public interface IOrderRepository : IRepository<Order>
    {
        void RemoveItems(IEnumerable<OrderItem> items);

        Task AddItemsAsync(IEnumerable<OrderItem> items);
    }
}
