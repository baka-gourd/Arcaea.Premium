namespace Arcaea.Premium;

public class DirectoryUtil
{
    public static string AppDatabase { get; } = $"{FileSystem.AppDataDirectory}/data.db";
    public static string SongDirectory { get; } = $"{FileSystem.AppDataDirectory}/songs";
}