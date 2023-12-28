
namespace Presentation.ViewModels.Order
{
    public class OrderViewModel : ViewModelBase<Guid>
    {
        public IReadOnlyCollection<OrderItemViewModel> OrderItems { get; set; } = Array.Empty<OrderItemViewModel>();
    }

    public class OrderItemViewModel
    {
        public long ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}