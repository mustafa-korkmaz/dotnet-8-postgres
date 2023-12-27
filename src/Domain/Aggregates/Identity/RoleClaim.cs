
namespace Domain.Aggregates.Identity
{
    public class RoleClaim : ValueObjectBase
    {
        public int RoleId { get; private set; }

        public Role? Role { get; private set; }

        public int ClaimId { get; private set; }
        public Claim? Claim { get; private set; }

        public RoleClaim(int roleId, int claimId)
        {
            ClaimId = claimId;
            RoleId = roleId;
        }
    }
}
