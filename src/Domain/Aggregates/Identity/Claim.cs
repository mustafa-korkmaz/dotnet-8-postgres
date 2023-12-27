
namespace Domain.Aggregates.Identity
{
    public class Claim : EntityBase<int>
    {
        public string Name { get; private set; }

        public Claim(string name)
        {
            Name = name;
        }
    }
}
