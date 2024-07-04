using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NewsXamarinCVT.Droid;
using NewsXamarinCVT.Models;
using NewsXamarinCVT.Datos;
using Xamarin.Forms;

[assembly: Dependency(typeof(LatestVersionCheck))]
namespace NewsXamarinCVT.Droid
{
    public class LatestVersionCheck : ILatest
    {
        string _packageName => global::Android.App.Application.Context.PackageName;
        string _versionName => global::Android.App.Application.Context.PackageManager.GetPackageInfo(global::Android.App.Application.Context.PackageName, 0).VersionName;

        public string InstalledVersionNumber
        {
            get => _versionName;
        }
  
        public async Task<bool> IsUsingLatestVersion()
        {
            
            bool VercionO = false;

            DatosApp dp = new DatosApp();
            string VS = dp.TraeVersion();
            if (_versionName.Equals(VS))
            {
                VercionO = true;
            }        
            return VercionO;
        }
        public Task OpenAppInStore()
        {
            return OpenAppInStore(_packageName);
        }
        public Task OpenAppInStore(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentNullException(nameof(appName));
            }

            try
            {
                var intent = new Intent(Intent.ActionView, global::Android.Net.Uri.Parse($"market://details?id={appName}"));
                intent.SetPackage("com.android.vending");
                intent.SetFlags(ActivityFlags.NewTask);
                global::Android.App.Application.Context.StartActivity(intent);
            }
            catch (ActivityNotFoundException)
            {
                var intent = new Intent(Intent.ActionView, global::Android.Net.Uri.Parse($"https://play.google.com/store/apps/details?id={appName}"));
                global::Android.App.Application.Context.StartActivity(intent);
            }

            return Task.FromResult(true);
        }
    }
}