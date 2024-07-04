using Android.App;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: Application(UsesCleartextTraffic = true)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]
//[assembly: UsesPermission(Android.Manifest.Permission.Flashlight)]


[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesFeature("android.hardware.location", Required = false)]
[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
[assembly: UsesFeature("android.hardware.location.network", Required = false)]
//#if DEBUG
//[assembly: Application(Debuggable = true, UsesCleartextTraffic = true)]
//#else
//[assembly: Application(Debuggable=false, UsesCleartextTraffic = true)]
//#endif