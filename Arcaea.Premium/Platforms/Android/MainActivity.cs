using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Color = Android.Graphics.Color;

namespace Arcaea.Premium
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public override void OnCreate(Bundle? savedInstanceState, PersistableBundle? persistentState)
        {
            Window?.SetFlags(WindowManagerFlags.TranslucentStatus, WindowManagerFlags.TranslucentStatus);
            Window?.SetStatusBarColor(Color.Transparent);
            Window?.SetNavigationBarColor(Color.Transparent);
            base.OnCreate(savedInstanceState, persistentState);
        }
    }
}