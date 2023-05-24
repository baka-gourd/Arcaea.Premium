#nullable disable
using Microsoft.EntityFrameworkCore;

using TempTests.Models.StModels;

namespace Arcaea.Premium.Models.DataModels;

public class AppDataContext : DbContext
{
    public DbSet<Cleartype> ClearTypes { get; set; }

    public DbSet<Score> Scores { get; set; }

    public AppDataContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={DirectoryUtil.AppDatabase};");
}