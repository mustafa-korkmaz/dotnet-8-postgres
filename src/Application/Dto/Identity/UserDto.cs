
namespace Application.Dto.Identity
{
    public class UserDto : DtoBase<Guid>
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? NameSurname { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string PasswordHash { get; set; } = null!;

        public ICollection<string> Claims { get; set; } = new List<string>();
    }
}
