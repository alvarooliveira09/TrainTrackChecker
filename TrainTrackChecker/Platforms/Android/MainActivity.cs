using Android.App;
using Android.Content.PM;
using Android.OS;

namespace TrainTrackChecker {
    [Activity(
        Theme = "@style/Maui.SplashTheme",
        MainLauncher = true,
        ConfigurationChanges =
        ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
        ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]

    public class MainActivity : MauiAppCompatActivity {
        
    }
}


    //https://learn.microsoft.com/en-us/answers/questions/990167/lock-screen-orientation-net-maui

    //[Activity(
    //    Theme = "@style/Maui.SplashTheme",
    //    MainLauncher = true,
    //    ConfigurationChanges = ConfigChanges.ScreenSize  | ConfigChanges.UiMode | 
    //    ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density
    //    | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]