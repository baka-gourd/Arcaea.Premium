using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Arcaea.Premium.Models.StModels;
using Arcaea.Premium.Models.Wiki;

using Microsoft.EntityFrameworkCore;

namespace Arcaea.Premium.Models.DataModels;

public class AppDataContext : DbContext
{
    public DbSet<Cleartype> ClearTypes { get; set; } = null!;

    public DbSet<Score> Scores { get; set; } = null!;

    public DbSet<SongData> SongList { get; set; } = null!;

    public DbSet<SongConstantValueTuple> SongConstantValueTuples { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={DirectoryUtil.AppDatabase};");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<SongData>()
            .Property(x => x.TitleLocalizationDict)
            .HasConversion(v => JsonSerializer.Serialize(v, SongContext.Default.DictionaryStringString!)
                , v => JsonSerializer.Deserialize(v, SongContext.Default.DictionaryStringString));

        modelBuilder.Entity<SongData>()
            .Property(x => x.Difficulties)
            .HasConversion(v => JsonSerializer.Serialize(v, SongDataContext.Default.DifficultyArray!),
                v => JsonSerializer.Deserialize(v, SongDataContext.Default.DifficultyArray));
    }
}

public class SongData
{
    public long Idx { get; set; }
    public string? Id { get; set; }
    public Dictionary<string, string>? TitleLocalizationDict { get; set; }
    public string? Artist { get; set; }
    public string? Bpm { get; set; }
    public double BpmBase { get; set; }
    public string? Set { get; set; }
    public string? Purchase { get; set; }
    public long AudioPreview { get; set; }
    public long AudioPreviewEnd { get; set; }
    public long Side { get; set; }
    public string? Bg { get; set; }
    public string? BgInverse { get; set; }
    public long Date { get; set; }
    public string? Version { get; set; }
    public Difficulty[]? Difficulties { get; set; }
    public bool? WorldUnlock { get; set; }
    public bool? BeyondUnLock { get; set; }
    public bool? RemoteDownload { get; set; }
    public List<SongConstantValueTuple> ConstantValueTuples { get; set; } = new();
}

[JsonSerializable(typeof(SongData))]
public partial class SongDataContext : JsonSerializerContext
{
}