
namespace Application.Dto.Order
{
    public class OrderItemDto
    {
        public long ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}
