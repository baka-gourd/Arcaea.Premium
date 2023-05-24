using Arcaea.Premium.Models.DataModels;
using Arcaea.Premium.Resources.Localization;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;

using TempTests.Models.StModels;

namespace Arcaea.Premium.Pages
{
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

        public RelayCommand St3Command { get; }

        public ImportPageViewModel()
        {
            St3Command = new(St3Button_Clicked, () => IsSt3ButtonEnabled);
        }

        private async void St3Button_Clicked()
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
                var db = new St3Context();
                var app = new AppDataContext();
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
                db.Dispose();
            });
        }
    }
}
