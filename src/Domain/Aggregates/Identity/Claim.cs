
namespace Domain.Aggregates.Identity
{
    public class Claim : EntityBase<int>
    {
        public string Name { get; private set; }

        public Claim(string name, DateTimeOffset createdAt)
        {
            Name = name;
            CreatedAt = createdAt;
        }

        public Claim(int id, string name, DateTimeOffset createdAt) : base(id)
        {
            Name = name;
            CreatedAt = createdAt;
        }
    }
}
