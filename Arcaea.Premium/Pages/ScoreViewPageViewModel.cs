using Arcaea.Premium.Models.DataModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Arcaea.Premium.Pages;

public class SongViewItem
{
    public byte[] ImageData { get; set; } = Array.Empty<byte>();
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string FullName => $"{Name} ({Id})";
    public required string Artist { get; set; }
    public long Pst { get; set; }
    public string PstStr => $"PST: {Pst}";
    public long Prs { get; set; }
    public string PrsStr => $"PRS: {Prs}";
    public long Ftr { get; set; }
    public string FtrStr => $"FTR: {Ftr}";
    public long Byd { get; set; }
    public string BydStr => $"BYD: {Byd}";

    public static readonly SongViewItem Default = new() { Name = "", Id = "", Artist = "" };
}

public class ScoreViewPageViewModel : ObservableObject
{
    private List<SongViewItem> _songData = new();

    public List<SongViewItem> SongData
    {
        get => _songData;
        set => SetProperty(ref _songData, value);
    }

    private bool _isRefreshing = false;

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetProperty(ref _isRefreshing, value);
    }

    public RelayCommand RefreshCommand { get; set; }

    public ScoreViewPageViewModel()
    {
        RefreshCommand = new(GetData, () => !IsRefreshing);
        Task.Run(GetData);
    }

    private void GetData()
    {
#pragma warning disable CS8601
        var collection = DataBase.AppDataBase.SongList
            .SelectMany(song => DataBase.AppDataBase.Scores.Where(s => s.SongId == song.Id),
                        (song, score) => new { song, score })
                    .Select(tuple => new SongViewItem
                    {
                        Artist = tuple.song.Artist ?? "none",
                        // ReSharper disable once CanSimplifyDictionaryLookupWithTryGetValue
                        Name = tuple.song.TitleLocalizationDict == null ? tuple.song.Id : tuple.song.TitleLocalizationDict.ContainsKey("zh-Hans") ? tuple.song.TitleLocalizationDict["zh-Hans"] :
                            // ReSharper disable once CanSimplifyDictionaryLookupWithTryGetValue
                            tuple.song.TitleLocalizationDict.ContainsKey("ja") ? tuple.song.TitleLocalizationDict["ja"] :
                            tuple.song.TitleLocalizationDict["en"],
                        Id = tuple.song.Id!,
                        Pst = tuple.score.SongDifficulty == 0 ? tuple.score.Score1.GetValueOrDefault(0) : 0,
                        Prs = tuple.score.SongDifficulty == 1 ? tuple.score.Score1.GetValueOrDefault(0) : 0,
                        Ftr = tuple.score.SongDifficulty == 2 ? tuple.score.Score1.GetValueOrDefault(0) : 0,
                        Byd = tuple.score.SongDifficulty == 3 ? tuple.score.Score1.GetValueOrDefault(0) : 0
                    });
#pragma warning restore CS8601
        SongData = collection.ToList().GroupBy(x => x.Id).Select(g =>
        new SongViewItem
        {
            Id = g.Key,
            Name = g.First().Name,
            Artist = g.First().Artist,
            Pst = (g.FirstOrDefault(x => x!.Pst != 0, null) ?? SongViewItem.Default).Pst,
            Prs = (g.FirstOrDefault(x => x!.Prs != 0, null) ?? SongViewItem.Default).Prs,
            Ftr = (g.FirstOrDefault(x => x!.Ftr != 0, null) ?? SongViewItem.Default).Ftr,
            Byd = (g.FirstOrDefault(x => x!.Byd != 0, null) ?? SongViewItem.Default).Byd,
        }).Where(x => x.Pst + x.Prs + x.Ftr + x.Byd > 0).OrderBy(x => x.Name).ToList();
        IsRefreshing = false;
    }
}