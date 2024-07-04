using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NewsXamarinCVT.Droid;
using NewsXamarinCVT.Models;
using Xamarin.Forms;
[assembly: Dependency(typeof(SettingServiceGP))]
namespace NewsXamarinCVT.Droid
{
    public class SettingServiceGP:ISettingsServiceAcGP
    {
        public SettingServiceGP()
        { }      
        public void OpenSettingsLocali()
        {
            Xamarin.Forms.Forms.Context.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionLocat‌​ionSourceSettings));
        }
     

    }
}