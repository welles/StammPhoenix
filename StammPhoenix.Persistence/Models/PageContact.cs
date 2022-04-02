using System.ComponentModel.DataAnnotations.Schema;

namespace StammPhoenix.Persistence.Models;

[Table("PAGE_CONTACT")]
public class PageContact : Entity
{
    [Column("NAME")]
    public string Name { get; set; }

    [Column("PHONE_NUMBER")]
    public string PhoneNumber { get; set; }
}
