namespace StammPhoenix.Persistence.Models
{
    public class PlannedEvent
    {
        public string PlannedEventId { get; set; }

        public Rank ParticipatingRanks { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }
    }
}
