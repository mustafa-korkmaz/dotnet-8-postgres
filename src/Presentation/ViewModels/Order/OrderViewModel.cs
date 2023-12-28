
namespace Presentation.ViewModels.Order
{
    public class OrderViewModel : ViewModelBase<Guid>
    {
        public decimal Price { get; set; }

        public string UserId { get; set; } = null!;

        public IReadOnlyCollection<OrderItemViewModel> Items { get; set; } = Array.Empty<OrderItemViewModel>();
    }

    public class OrderItemViewModel
    {
        public long ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}