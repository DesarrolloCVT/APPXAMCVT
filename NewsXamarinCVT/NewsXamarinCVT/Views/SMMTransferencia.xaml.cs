﻿using NewsXamarinCVT.Datos;
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
    public partial class SMMTransferencia : ContentPage
    {
        public SMMTransferencia()
        {
            InitializeComponent();
           
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            txtFolioTransferencia.Text = string.Empty;
             
        }
    

        protected override bool OnBackButtonPressed()
        {
            //return true to prevent back, return false to just do something before going back. 
            return true;
        }

        private void btn_Nuevo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SMM_TransferenciaCabecera { Title = "Finalizar" });
        }

        private async void btn_Reanudar_Clicked(object sender, EventArgs e)
        {
            if (txtFolioTransferencia.Text.Equals(string.Empty))
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
               await DisplayAlert("Alerta", "ingrese un numero de Transferencia", "Aceptar");
            }
            else
            {
                var ACC = Connectivity.NetworkAccess;
                if (ACC == NetworkAccess.Internet)
                {


                    DatosTransferenciaSMM rc = new DatosTransferenciaSMM();

                    int FolioTrans = rc.ValidaTransferencia(Convert.ToInt32(txtFolioTransferencia.Text),1);

                    if (FolioTrans != 0)
                    {
                      await  Navigation.PushAsync(new SMM_TansferenciaDetalle(FolioTrans) { Title = "Finalizar" });
                    }
                    else
                    {
                      await  DisplayAlert("Alerta", "Transferencia no existe o se encuentra cerrada, Favor verificar", "Aceptar");
                        txtFolioTransferencia.Text = string.Empty;
                        txtFolioTransferencia.Focus();
                    }
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                   await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
                }
            }
        }
    }
}