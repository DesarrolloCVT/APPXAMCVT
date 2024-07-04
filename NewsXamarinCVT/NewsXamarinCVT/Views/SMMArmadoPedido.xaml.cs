using NewsXamarinCVT.Datos;
using NewsXamarinCVT.Models;
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
    public partial class SMMArmadoPedido : ContentPage
    {
        public SMMArmadoPedido()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {

            base.OnAppearing();
           
            txtNPedido.Text = string.Empty;
        }


        private void btn_Crear_Clicked(object sender, EventArgs e)
        {
            if (txtNPedido.Text.Equals(string.Empty))
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "ingrese un numero de Recepcion", "Aceptar");
            }
            else
            {
                var ACC = Connectivity.NetworkAccess;
                if (ACC == NetworkAccess.Internet)
                {


                    DatosSMM_ArmadoPedido ar = new DatosSMM_ArmadoPedido();

                    int FolioPed = ar.ValidaFolioPrdido(Convert.ToInt32(txtNPedido.Text));

                    if (FolioPed != 0)
                    {
                        Navigation.PushAsync(new SMMArmadoPedidoDetalle(FolioPed) { Title = "Finalizar" });
                    }
                    else
                    {
                        DisplayAlert("Alerta", "Pedido no existe o se encuentra cerrada, Favor verificar", "Aceptar");
                    }
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            //return true to prevent back, return false to just do something before going back. 
            return true;
        }

    }
}