﻿using NewsXamarinCVT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsXamarinCVT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeoLoc : ContentPage
    {
        public GeoLoc()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            lblLat.Text = string.Empty;
            lblLong.Text = string.Empty;
            lblAlt.Text = string.Empty;
        }

        public async void btn_clicked(object sender, System.EventArgs e)
        {
            lblLat.Text = string.Empty;
            lblLong.Text = string.Empty;
            lblAlt.Text = string.Empty;

            //try
            //{
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                   

                    lblLat.Text += location.Latitude.ToString();
                    lblLong.Text += location.Longitude.ToString();
                    lblAlt.Text += location.Altitude.ToString();


                   //https://www.google.cl/maps/@x
                }
                else { await DisplayAlert("Confirmar", "Active GPS", "OK"); }
            //}
            //catch (FeatureNotSupportedException fnsEx)
            //{
            //    await DisplayAlert("Confirmar", "Active GPS", "OK");
            //}
            //catch (PermissionException pEx)
            //{
            //    await DisplayAlert("Confirmar", "Active GPS", "OK");
            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("Confirmar", "no se a encontrado localizacion", "OK");
            //}
        }

        private async void Mapa_Clicked(object sender, EventArgs e)
        {
            var location = new Location(Convert.ToDouble(lblLat.Text),Convert.ToDouble(lblLong.Text));
            var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };
            await Map.OpenAsync(location, options);

        }

        protected override bool OnBackButtonPressed()
        {
            //return true to prevent back, return false to just do something before going back. 
            return true;
        }
    }
}