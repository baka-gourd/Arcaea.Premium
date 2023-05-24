using System.Text.Json;

using Arcaea.Premium.Models.DataModels;
using Arcaea.Premium.Models.Wiki;
using Arcaea.Premium.Resources.Localization;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Arcaea.Premium.Pages;

internal class ImportPageViewModel : ObservableObject
{
    private string? _stButtonText = AppResource.ResourceManager.GetString("Import.Desc1");

    public string? St3ButtonText { get => _stButtonText; set => SetProperty(ref _stButtonText, value); }

    private bool _isSt3ButtonEnabled = true;

    public bool IsSt3ButtonEnabled
    {
        get => _isSt3ButtonEnabled;
        set => SetProperty(ref _isSt3ButtonEnabled, value);
    }

    private bool _isLogVisible = false;

    public bool IsLogVisible
    {
        get => _isLogVisible;
        set => SetProperty(ref _isLogVisible, value);
    }

    private string _logText = string.Empty;

    public string LogText
    {
        get => _logText;
        set => SetProperty(ref _logText, value);
    }

    private bool _isWikiDownloadEnabled = true;

    public bool IsWikiDownloadEnabled
    {
        get => _isWikiDownloadEnabled;
        set => SetProperty(ref _isWikiDownloadEnabled, value);
    }

    public RelayCommand St3Command { get; }
    public RelayCommand DownloadSongDataCommand { get; }

    public ImportPageViewModel()
    {
        St3Command = new(ImportSt3Data, () => IsSt3ButtonEnabled);
        DownloadSongDataCommand = new(DownloadSongData, () => IsWikiDownloadEnabled);
    }

    private async void ImportSt3Data()
    {
        FileResult? file = null;
        try
        {
            file = await FilePicker.Default.PickAsync(new PickOptions()
            {
                PickerTitle = "Select a st3 file"
            });
        }
        catch
        {
            // NOOP
        }

        if (file is not { } openedFile) return;
        IsSt3ButtonEnabled = false;
        var stream = await openedFile.OpenReadAsync();

        await Task.Run(() =>
        {
            if (File.Exists($"{FileSystem.CacheDirectory}/st3.db"))
            {
                File.Delete($"{FileSystem.CacheDirectory}/st3.db");
            }
            IsLogVisible = true;
            LogText = string.Empty;
            var bytes = new byte[stream.Length];
            _ = stream.Read(bytes, 0, bytes.Length);
            File.WriteAllBytes($"{FileSystem.CacheDirectory}/st3.db", bytes);
            LogText += "Load file...\n";
            LogText += "Read Database...\n";
            var db = DataBase.St3DataBase;
            var app = DataBase.AppDataBase;
            app.Database.EnsureCreated();
            LogText += "Import clear information...\n";
            foreach (var clearType in db.Cleartypes)
            {
                var list = from data in app.ClearTypes
                    where data.SongId == clearType.SongId
                    where data.SongDifficulty == clearType.SongDifficulty
                    select data;
                var obj = list.Any() ? list.First() : null;
                if (obj is null)
                {
                    app.ClearTypes.Add(clearType);
                }
                else
                {
                    obj.Ct = clearType.Ct;
                    obj.ClearType1 = clearType.ClearType1;
                    app.ClearTypes.Update(obj);
                }
            }

            LogText += "Import score information...\n";
            foreach (var score in db.Scores)
            {
                var list = from data in app.Scores
                    where data.SongId == score.SongId
                    where data.SongDifficulty == score.SongDifficulty
                    select data;
                var obj = list.Any() ? list.First() : null;
                if (obj is null)
                {
                    app.Scores.Add(score);
                }
                else
                {
                    obj.Ct = score.Ct;
                    obj.Date = score.Date;
                    obj.Health = score.Health;
                    obj.Modifier = score.Modifier;
                    obj.MissCount = score.MissCount;
                    obj.PerfectCount = score.PerfectCount;
                    obj.NearCount = score.NearCount;
                    obj.ShinyPerfectCount = score.ShinyPerfectCount;
                    obj.Version = score.Version;
                    obj.Score1 = score.Score1;
                    app.Scores.Update(obj);
                }
            }

            app.SaveChanges();
            LogText += "All Imported.\n";
            IsSt3ButtonEnabled = true;
        });
    }

    private async void DownloadSongData()
    {
        IsLogVisible = true;
        try
        {
            LogText = "Fetching song data from wiki...\n";
            const string songDataUrl =
                "https://wiki.arcaea.cn/api.php?action=query&prop=revisions&pageids=3706&format=json&rvprop=content";
            var result1 = await new HttpClient().GetStreamAsync(songDataUrl);
            var obj1 = await JsonSerializer.DeserializeAsync(result1, WikiRespContext.Default.WikiResp);
            if (obj1 is not { } data1)
            {
                return;
            }
            var songData = data1.Query!.Pages!["3706"].Revisions![0].Result;
            LogText += "Successful download song data.\n";
            LogText += "Fetching song constant values from wiki...\n";
            const string songConstantUrl =
                "https://wiki.arcaea.cn/api.php?action=query&prop=revisions&pageids=3582&format=json&rvprop=content";
            var result2 = await new HttpClient().GetStreamAsync(songConstantUrl);
            var obj2 = await JsonSerializer.DeserializeAsync(result2, WikiRespContext.Default.WikiResp);
            if (obj2 is not { } data2)
            {
                return;
            }
            var songConstantData = data2.Query!.Pages!["3582"].Revisions![0].Result;
            LogText += "Successful download song constant values.\n";
            var constantDict = JsonSerializer.Deserialize(songConstantData!,
                SongConstantValuesContext.Default.DictionaryStringSongConstantValueTupleArray);
            var songs = JsonSerializer.Deserialize(songData!, SongContext.Default.SongWrapper);
            if (songs is null || constantDict is null)
            {
                LogText += "Filed to deserialize data.\n";
                return;
            }
            var app = DataBase.AppDataBase;
            await app.Database.EnsureCreatedAsync();
            foreach (var song in songs.Songs!)
            {
                if (song.Id is null)
                {
                    continue;
                }
                if (constantDict.TryGetValue(song.Id, out var constants))
                {
                    var songConstantValueTuples = constants.Select(c => c is null ? new SongConstantValueTuple { Constant = 0, Old = false } : c).ToList();
                    var list = from data in app.SongList
                        where data.Id == song.Id
                        select data;
                    var obj = list.Any() ? list.First() : null;
                    if (obj is null)
                    {
                        SongData data = new()
                        {
                            Id = song.Id,
                            Idx = song.Idx,
                            Date = song.Date,
                            Artist = song.Artist,
                            AudioPreview = song.AudioPreview,
                            AudioPreviewEnd = song.AudioPreviewEnd,
                            Bg = song.Bg,
                            BgInverse = song.BgInverse,
                            Bpm = song.Bpm,
                            BpmBase = song.BpmBase,
                            TitleLocalizationDict = song.TitleLocalizationDict ?? new Dictionary<string, string>(),
                            Set = song.Set,
                            Purchase = song.Purchase,
                            Side = song.Side,
                            Version = song.Version,
                            Difficulties = song.Difficulties ?? Array.Empty<Difficulty>(),
                            WorldUnlock = song.WorldUnlock,
                            BeyondUnLock = song.BeyondUnLock,
                            RemoteDownload = song.RemoteDownload,
                            ConstantValueTuples = songConstantValueTuples
                        };
                        app.SongList.Add(data);
                    }
                    else
                    {
                        obj.Id = song.Id;
                        obj.Idx = song.Idx;
                        obj.Date = song.Date;
                        obj.Artist = song.Artist;
                        obj.AudioPreview = song.AudioPreview;
                        obj.AudioPreviewEnd = song.AudioPreviewEnd;
                        obj.Bg = song.Bg;
                        obj.BgInverse = song.BgInverse;
                        obj.Bpm = song.Bpm;
                        obj.BpmBase = song.BpmBase;
                        obj.TitleLocalizationDict = song.TitleLocalizationDict ?? new Dictionary<string, string>();
                        obj.Set = song.Set;
                        obj.Purchase = song.Purchase;
                        obj.Side = song.Side;
                        obj.Version = song.Version;
                        obj.Difficulties = song.Difficulties ?? Array.Empty<Difficulty>();
                        obj.WorldUnlock = song.WorldUnlock;
                        obj.BeyondUnLock = song.BeyondUnLock;
                        obj.RemoteDownload = song.RemoteDownload;
                        obj.ConstantValueTuples = songConstantValueTuples;
                        app.SongList.Update(obj);
                    }
                }
            }
            await app.SaveChangesAsync();
        }
        catch (Exception e)
        {
            LogText = $"{e.Message}\n";
            LogText += $"{e.Source}\n";
            LogText += $"{e.StackTrace}\n";
        }
    }
}