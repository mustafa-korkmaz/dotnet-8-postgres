
namespace Domain.Aggregates.Identity
{
    public class RoleClaim : EntityBase<int>
    {
        public int RoleId { get; private set; }

        public Role? Role { get; private set; }

        public int ClaimId { get; private set; }
        public Claim? Claim { get; private set; }

        public RoleClaim(int roleId, int claimId, DateTimeOffset createdAt)
        {
            ClaimId = claimId;
            RoleId = roleId;
            CreatedAt = createdAt;
        }

        public RoleClaim(int id, int roleId, int claimId, DateTimeOffset createdAt) : base(id)
        {
            ClaimId = claimId;
            RoleId = roleId;
            CreatedAt = createdAt;
        }
    }
}
