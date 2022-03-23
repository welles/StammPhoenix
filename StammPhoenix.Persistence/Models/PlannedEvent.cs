using StammPhoenix.Persistence.Constants;

namespace StammPhoenix.Persistence.Models
{
    public class PlannedEvent
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Rank ParticipatingRanks { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }
    }
}
