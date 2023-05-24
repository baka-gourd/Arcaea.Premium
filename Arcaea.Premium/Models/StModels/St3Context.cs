#nullable disable
using Microsoft.EntityFrameworkCore;

namespace Arcaea.Premium.Models.StModels;

public partial class St3Context : DbContext
{
    public St3Context()
    {
    }

    public St3Context(DbContextOptions<St3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cleartype> Cleartypes { get; set; }

    public virtual DbSet<Purchaseentry> Purchaseentries { get; set; }

    public virtual DbSet<Schemaversion> Schemaversions { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={FileSystem.CacheDirectory}/st3.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cleartype>(entity =>
        {
            entity.ToTable("cleartypes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClearType1)
                .HasColumnType("INT")
                .HasColumnName("clearType");
            entity.Property(e => e.Ct)
                .HasDefaultValueSql("0")
                .HasColumnType("INT")
                .HasColumnName("ct");
            entity.Property(e => e.SongDifficulty)
                .HasColumnType("INT")
                .HasColumnName("songDifficulty");
            entity.Property(e => e.SongId).HasColumnName("songId");
        });

        modelBuilder.Entity<Purchaseentry>(entity =>
        {
            entity.ToTable("purchaseentries");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Checksum).HasColumnName("checksum");
            entity.Property(e => e.Receipt).HasColumnName("receipt");
            entity.Property(e => e.ReceiptCipheredPayload).HasColumnName("receiptCipheredPayload");
            entity.Property(e => e.Sku).HasColumnName("sku");
        });

        modelBuilder.Entity<Schemaversion>(entity =>
        {
            entity.HasKey(e => e.AppliedVersion);

            entity.ToTable("schemaversion");

            entity.Property(e => e.AppliedVersion)
                .ValueGeneratedNever()
                .HasColumnType("INT")
                .HasColumnName("appliedVersion");
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.ToTable("scores");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ct)
                .HasDefaultValueSql("0")
                .HasColumnType("INT")
                .HasColumnName("ct");
            entity.Property(e => e.Date)
                .HasColumnType("INT")
                .HasColumnName("date");
            entity.Property(e => e.Health)
                .HasColumnType("INT")
                .HasColumnName("health");
            entity.Property(e => e.MissCount)
                .HasColumnType("INT")
                .HasColumnName("missCount");
            entity.Property(e => e.Modifier)
                .HasColumnType("INT")
                .HasColumnName("modifier");
            entity.Property(e => e.NearCount)
                .HasColumnType("INT")
                .HasColumnName("nearCount");
            entity.Property(e => e.PerfectCount)
                .HasColumnType("INT")
                .HasColumnName("perfectCount");
            entity.Property(e => e.Score1)
                .HasColumnType("INT")
                .HasColumnName("score");
            entity.Property(e => e.ShinyPerfectCount)
                .HasColumnType("INT")
                .HasColumnName("shinyPerfectCount");
            entity.Property(e => e.SongDifficulty)
                .HasColumnType("INT")
                .HasColumnName("songDifficulty");
            entity.Property(e => e.SongId).HasColumnName("songId");
            entity.Property(e => e.Version)
                .HasColumnType("INT")
                .HasColumnName("version");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
