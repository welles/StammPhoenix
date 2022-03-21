#pragma warning disable CS8618 // Disable nullable warning
namespace StammPhoenix.Persistence.Models
{
    public class LoginUser
    {
        public string Id { get; set; }

        public string NormalizedId { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public bool IsLocked { get; set; }

        public bool NeedPasswordChange { get; set; }
    }
}
