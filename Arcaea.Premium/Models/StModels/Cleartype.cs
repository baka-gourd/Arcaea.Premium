using System.ComponentModel.DataAnnotations;

namespace Arcaea.Premium.Models.StModels;

public partial class Cleartype
{
    [Key]
    public long Id { get; set; }

    public string? SongId { get; set; }

    public long? SongDifficulty { get; set; }

    public long? ClearType1 { get; set; }

    public long? Ct { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Cleartype c)
        {
            return false;
        }

        return c.SongId!.Equals(SongId) && c.SongDifficulty.Equals(SongDifficulty);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
