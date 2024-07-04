using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NewsXamarinCVT.Droid;
using NewsXamarinCVT.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetSSIDAndroid))]
namespace NewsXamarinCVT.Droid
{
    public class GetSSIDAndroid : IGetSSID
    {
        public string GetSSID()
        {
            WifiManager wifiManager = (WifiManager)(Android.App.Application.Context.GetSystemService(Context.WifiService));

            if (wifiManager != null && !string.IsNullOrEmpty(wifiManager.ConnectionInfo.SSID))
            {
                return wifiManager.ConnectionInfo.SSID;
            }
            else
            {
                return "NULL";
            }
        }
    }
}