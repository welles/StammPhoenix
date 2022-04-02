using System.ComponentModel.DataAnnotations.Schema;
using StammPhoenix.Persistence.Constants;

namespace StammPhoenix.Persistence.Models
{
    public class PlannedEvent : Entity
    {
        [Column("NAME")]
        public string Name { get; set; }

        [Column("PARTICIPATING_RANKS")]
        public Rank ParticipatingRanks { get; set; }

        [Column("START_DATE")]
        public DateOnly StartDate { get; set; }

        [Column("END_DATE")]
        public DateOnly? EndDate { get; set; }
    }
}
