using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StammPhoenix.Persistence.Models;

public abstract class Entity
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; internal set; }

    [Column("CREATED_ON")]
    public DateTime CreatedOn { get; internal set; }

    [Column("CREATED_BY")]
    public string CreatedBy { get; internal set; }

    [Column("MODIFIED_ON")]
    public DateTime? ModifiedOn { get; internal set; }

    [Column("MODIFIED_BY")]
    public string? ModifiedBy { get; internal set; }
}
