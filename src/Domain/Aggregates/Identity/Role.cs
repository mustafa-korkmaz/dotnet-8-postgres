
namespace Domain.Aggregates.Identity
{
    public class Role : EntityBase<int>
    {
        public string Name { get; private set; }

        private ICollection<RoleClaim> _claims;
        public IReadOnlyCollection<RoleClaim> Claims
        {
            get => _claims.ToList();
            private set => _claims = value.ToList();
        }

        public Role(string name, DateTimeOffset createdAt)
        {
            _claims = new List<RoleClaim>();
            Name = name;
            CreatedAt = createdAt;
        }

        public Role(int id, string name, DateTimeOffset createdAt) : base(id)
        {
            _claims = new List<RoleClaim>();
            Name = name;
            CreatedAt = createdAt;
        }
    }
}
