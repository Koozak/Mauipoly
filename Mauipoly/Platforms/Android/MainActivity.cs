using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Mauipoly
{
    [Activity(Theme = "@style/Maui.SplashTheme", ResizeableActivity = true, LaunchMode = LaunchMode.SingleTask, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : MauiAppCompatActivity
    {

    }
}
