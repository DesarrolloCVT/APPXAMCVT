﻿using NewsXamarinCVT.Datos;
using NewsXamarinCVT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsXamarinCVT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResumenConsultaUbicacion : ContentPage
    {
        public ResumenConsultaUbicacion(string idpos)
        {
            InitializeComponent();
            cargadatos(idpos);
            //string g = idpos;
        }

        void cargadatos(string idPosicion)
        {
            DatosConsultaUbicacion dCu = new DatosConsultaUbicacion();
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {

                DataTable dt = dCu.ResumenConsultaUbicacion(Convert.ToInt32(idPosicion));
                
                GvData.ItemsSource = dt;
                GvData.Columns["Layout_Id"].IsVisible = false;
                GvData.Columns["ArticleProvider_CodClient"].Caption = "Cod.Producto";
                GvData.Columns["ArticleProvider_CodClient"].Width = 110;
                GvData.Columns["ArticleProvider_Description"].Caption = "Producto";
                GvData.Columns["ArticleProvider_Description"].Width = 110;
                GvData.Columns["Cantidad"].Caption = "Cantidad";
                GvData.Columns["Cantidad"].Width = 110;
                GvData.Columns["Bultos"].Caption = "Bultos";
                GvData.Columns["Bultos"].Width = 110;
                GvData.Columns["Cantidad_Pedidos"].IsVisible = false;

            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }



        }
    }
}