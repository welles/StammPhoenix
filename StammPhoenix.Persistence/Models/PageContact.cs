using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StammPhoenix.Persistence.Models;

public class PageContact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; internal set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }
}
