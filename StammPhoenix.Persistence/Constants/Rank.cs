namespace StammPhoenix.Persistence.Constants
{
    [Flags]
    public enum Rank
    {
        Woelflinge = 1 << 0,
        Jungpfadfinder = 1 << 1,
        Pfadfinder = 1 << 2,
        Rover = 1 << 3,
        Leiter = 1 << 4,
        Extern = 1 << 5,
        Ehemalige = 1 << 6,
        Verein = 1 << 7
    }
}
