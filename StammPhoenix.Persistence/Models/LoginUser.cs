using System.ComponentModel.DataAnnotations.Schema;
using StammPhoenix.Persistence.Constants;

namespace StammPhoenix.Persistence.Models;

[Table("LOGIN_USER")]
public class LoginUser : Entity
{
    [Column("NAME")]
    public string Name { get; set; }

    [Column("EMAIL")]
    public string Email { get; set; }

    [Column("PASSWORD_HASH")]
    public string PasswordHash { get; set; }

    [Column("IS_LOCKED")]
    public bool IsLocked { get; set; }

    [Column("NEED_PASSWORD_CHANGE")]
    public bool NeedPasswordChange { get; set; }

    [Column("ROLE")]
    public Role Role { get; set; }
}
