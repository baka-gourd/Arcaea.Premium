namespace Arcaea.Premium;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        Cache.InitCache();
        MainPage = new AppShell();
    }
}