
namespace Domain.Aggregates.Identity
{
    public class UserRole : EntityBase<int>
    {
        public Guid UserId { get; private set; }

        public User? User { get; private set; }

        public int RoleId { get; private set; }
        public Role? Role { get; private set; }

        public UserRole(Guid userId, int roleId, DateTimeOffset createdAt)
        {
            UserId = userId;
            RoleId = roleId;
            CreatedAt = createdAt;
        }

        public UserRole(int id, Guid userId, int roleId, DateTimeOffset createdAt) : base(id)
        {
            UserId = userId;
            RoleId = roleId;
            CreatedAt = createdAt;
        }
    }
}
