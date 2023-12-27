namespace Domain.Aggregates.Product
{
    public class Product : EntityBase<long>
    {
        public string Sku { get; private set; }

        public string Name { get; private set; }

        public decimal UnitPrice { get; private set; }

        public int StockQuantity { get; private set; }

        public Product(string sku, string name, decimal unitPrice, int stockQuantity, DateTimeOffset createdAt)
        {
            Sku = sku;
            Name = name;
            UnitPrice = unitPrice;
            StockQuantity = stockQuantity;
            CreatedAt = createdAt;
        }

        public void RemoveStock(int quantity)
        {
            StockQuantity -= quantity;
        }
    }
}