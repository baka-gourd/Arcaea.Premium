using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TempTests.Models.StModels;

public partial class Score
{
    [Key]
    public long Id { get; set; }

    public long? Version { get; set; }

    public long? Score1 { get; set; }

    public long? ShinyPerfectCount { get; set; }

    public long? PerfectCount { get; set; }

    public long? NearCount { get; set; }

    public long? MissCount { get; set; }

    public long? Date { get; set; }

    public string? SongId { get; set; }

    public long? SongDifficulty { get; set; }

    public long? Modifier { get; set; }

    public long? Health { get; set; }

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
