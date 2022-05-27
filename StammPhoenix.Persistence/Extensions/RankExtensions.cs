using StammPhoenix.Persistence.Constants;

namespace StammPhoenix.Persistence.Extensions;

public static class RankExtensions
{
    public static string ToDisplayName(this Rank rank, bool plural = false)
    {
        return rank switch
        {
            Rank.Woelflinge => plural ? "Wölflinge" : "Wölfling",
            Rank.Jungpfadfinder => plural ? "Jungpfadfinder" : "Jungpfadfinder:in",
            Rank.Pfadfinder => plural ? "Pfadfinder" : "Pfadfinder:in",
            Rank.Rover => plural ? "Rover" : "Rover:in",
            Rank.Leiter => plural ? "Leitende" : "Leiter:in",
            Rank.Extern => plural ? "Externe" : "Externe:r",
            Rank.Ehemalige => plural ? "Ehemalige" : "Ehemalige:r",
            Rank.Verein => plural ? "Vereinsmitglieder" : "Vereinsmitglied",
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
        };
    }
}
