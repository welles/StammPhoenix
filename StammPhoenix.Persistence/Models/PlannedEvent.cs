using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StammPhoenix.Persistence.Constants;

namespace StammPhoenix.Persistence.Models
{
    public class PlannedEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; internal set; }

        public string Name { get; set; }

        public Rank ParticipatingRanks { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }
    }
}
