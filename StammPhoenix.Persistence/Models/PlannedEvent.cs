using System.ComponentModel.DataAnnotations.Schema;
using StammPhoenix.Persistence.Constants;

namespace StammPhoenix.Persistence.Models;

[Table("PLANNED_EVENT")]
public class PlannedEvent : Entity
{
    [Column("NAME")]
    public string Name { get; internal set; }

    [Column("PARTICIPATING_RANKS")]
    public Rank ParticipatingRanks { get; internal set; }

    [Column("START_DATE")]
    public DateOnly StartDate { get; internal set; }

    [Column("END_DATE")]
    public DateOnly? EndDate { get; internal set; }
}
