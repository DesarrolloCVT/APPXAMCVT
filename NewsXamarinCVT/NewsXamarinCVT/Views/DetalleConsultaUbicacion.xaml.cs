using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NewsXamarinCVT.Datos;
using NewsXamarinCVT.Models;
using NewsXamarinCVT.Views;
using Acr.UserDialogs;

namespace NewsXamarinCVT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleConsultaUbicacion : ContentPage
    {
        string _iposid = "";
        public DetalleConsultaUbicacion(string idPosicion)
        {
            InitializeComponent();
            cargadatos(idPosicion);
            _iposid = idPosicion;
            //vCArga.IsVisible = false;
            //activity.IsEnabled = false;
            //activity.IsRunning = false;
            //activity.IsVisible = false;

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //vCArga.IsVisible = false;
            //activity.IsEnabled = false;
            //activity.IsRunning = false;
            //activity.IsVisible = false;
        }

        void cargadatos(string idPosicion)
        {
            DatosConsultaUbicacion dCu = new DatosConsultaUbicacion();
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {

                DataTable dt = dCu.DetalleConsultaUbicacion(Convert.ToInt32(idPosicion));
                GvData.ItemsSource = dt;

                GvData.Columns["Package_SSCC"].Caption = "N° de Pallet";
                GvData.Columns["Package_SSCC"].Width = 110;
                GvData.Columns["ArticleProvider_CodClient"].Caption = "Cod. Producto";
                GvData.Columns["ArticleProvider_CodClient"].Width = 110;
                GvData.Columns["Package_Quantity"].Caption = "Cantidad";
                GvData.Columns["Package_Quantity"].Width = 110;
                GvData.Columns["ArticleProvider_Description"].Caption = "Producto";
                GvData.Columns["ArticleProvider_Description"].Width = 110;
                GvData.Columns["Staff_Name"].Caption = "Usuario";
                GvData.Columns["Staff_Name"].Width = 110;
                string totalcoun= GvData.RowCount.ToString();               
                lblCantPallets.Text ="Cantidad Pallets: "+totalcoun;
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }



        }

        private async void BtnResumen_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Cargando"))
            {
                await Task.Delay(10);
                //vCArga.IsVisible = true;
                //activity.IsEnabled = true;
                //activity.IsRunning = true;
                //activity.IsVisible = true;
                 Navigation.PushAsync(new ResumenConsultaUbicacion(_iposid) { Title = "Volver" });
            }
                
        }
    }
}
