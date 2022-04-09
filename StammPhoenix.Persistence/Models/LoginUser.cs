using System.ComponentModel.DataAnnotations.Schema;
using StammPhoenix.Persistence.Constants;

namespace StammPhoenix.Persistence.Models;

[Table("LOGIN_USER")]
public class LoginUser : Entity
{
    [Column("NAME")]
    public string Name { get; internal set; }

    [Column("EMAIL")]
    public string Email { get; internal set; }

    [Column("PASSWORD_HASH")]
    public string PasswordHash { get; internal set; }

    [Column("IS_LOCKED")]
    public bool IsLocked { get; internal set; }

    [Column("NEED_PASSWORD_CHANGE")]
    public bool NeedPasswordChange { get; internal set; }

    [Column("ROLE")]
    public Role Role { get; internal set; }
}
