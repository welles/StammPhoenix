using System.ComponentModel.DataAnnotations.Schema;
using StammPhoenix.Persistence.Constants;

namespace StammPhoenix.Persistence.Models;

[Table("TEAM")]
public class Team : Entity
{
    [Column("RANK")]
    public Rank Rank { get; internal set; }

    [Column("MEMBERS")]
    public string? Members { get; internal set; }

    [Column("AGE_GROUP")]
    public string? AgeGroup { get; internal set; }

    [Column("MEETING_TIME")]
    public string? MeetingTime { get; internal set; }

    [Column("MEETING_PLACE")]
    public string? MeetingPlace { get; internal set; }

    [Column("IMAGE")]
    public byte[]? Image { get; internal set; }
}
