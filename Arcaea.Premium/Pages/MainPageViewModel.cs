using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Arcaea.Premium.Pages;

public class MainPageViewModel : ObservableObject
{
    private string _pttStr = "";
    public string PttStr
    {
        get => _pttStr;
        set => SetProperty(ref _pttStr, value);
    }
    public RelayCommand CalcPttCommand { get; set; }

    public MainPageViewModel()
    {
        CalcPttCommand = new(CalcB30,()=>true);
    }

    public void CalcB30()
    {
        var calcBest30 = Math.Round(Ptt.CalcBest30(),6);
        PttStr = calcBest30.ToString(CultureInfo.InvariantCulture);
    }
}