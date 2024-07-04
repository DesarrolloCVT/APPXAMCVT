using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using Plugin.Fingerprint;
using Plugin.CurrentActivity;

namespace NewsXamarinCVT.Droid
{
    [Activity(Label = "CVT WMS", Icon = "@drawable/Logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string[] Permission =
       {
            Android.Manifest.Permission.AccessFineLocation,
            Android.Manifest.Permission.AccessCoarseLocation,
            Android.Manifest.Permission.LocationHardware,
            Android.Manifest.Permission.ManageExternalStorage,
             Android.Manifest.Permission.ReadExternalStorage,
              Android.Manifest.Permission.WriteExternalStorage,


        };

        const int ResqId = 0;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            //FingerPrint
            CrossFingerprint.SetCurrentActivityResolver(() => CrossCurrentActivity.Current.Activity);
           

            Xamarin.Essentials.Platform.Init(this, bundle);
            RequestPermissions(Permission,ResqId);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            //DevExpress.Mobile.Forms.Init();
            DevExpress.XamarinForms.DataGrid.Initializer.Init();
            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();
       
            UserDialogs.Init(this);
            CrossCurrentActivity.Current.Init(this, bundle);


            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);       

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
       
    }
}