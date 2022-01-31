namespace StammPhoenix.Persistence.Models
{
    [Flags]
    public enum Rank
    {
        Woelfling = 1 << 0,
        Jungpfadfinder = 1 << 1,
        Pfadfinder = 1 << 2,
        Rover = 1 << 3,
        Leiter = 1 << 4,
        Extern = 1 << 5
    }
}
