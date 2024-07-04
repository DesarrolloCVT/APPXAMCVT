﻿using NewsXamarinCVT.Datos;
using NewsXamarinCVT.Models;
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

namespace NewsXamarinCVT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SMM_Picking_Consolidado_Cabecera : ContentPage
    {
        public SMM_Picking_Consolidado_Cabecera()
        {
            InitializeComponent();
            CargaDatosDpto();

        }
        protected override async void OnAppearing()
        {

            base.OnAppearing();
            cboDpto.SelectedIndex = -1;
            //GvDatos.IsVisible = false;
            btnPikear.IsEnabled = false;

        }
        void CargaDatosDpto()
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                DatosPickingConsolidadoSMM pi = new DatosPickingConsolidadoSMM();
                List<SMMDepartamentos> lt = pi.TraeDepartamentos();

                foreach (var t in lt)
                {
                    cboDpto.Items.Add(t.GroupName.ToString());

                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }

        private void FConsolidado_DateSelected(object sender, DateChangedEventArgs e)
        {

            cboDpto.Focus();
        }

        private void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            DatosPickingConsolidadoSMM tp = new DatosPickingConsolidadoSMM();
            // List<SMMPickingConsolidadoClass> lt = new List<SMMPickingConsolidadoClass>();

            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                

                string fecha = FConsolidado.Date.Year + "-" + FConsolidado.Date.Month + "-" + FConsolidado.Date.Day;
                string Depa = cboDpto.SelectedItem.ToString();
                DataTable dt = tp.DetallePickingSMM(fecha, Depa);
              

                if (dt.Rows.Count != 0)
                {
                    GvData.IsVisible = true;
                    GvData.ItemsSource = dt;

                    GvData.Columns["CodProducto"].Caption = "CodProducto";                  
                    GvData.Columns["Producto"].Caption = "Producto";
                    GvData.Columns["DeptoVentas"].IsVisible = false;
                    GvData.Columns["FechaEntrega"].IsVisible = false;
                    //GvData.RowHeight = 150;
                    //GvData.ColumnsAutoWidth = true;
                    GvData.Columns["CantidadSolicitada"].Caption = "Cant.Soli";
                    GvData.Columns["CantidadPikear"].Caption = "Cant.Pendiente";
                    //GvDa+lta.Columns["Package_SSCC"].IsVisible = false;
                  
                  
                    btnPikear.IsEnabled = true;
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "No se encontraron Datos", "Aceptar");
                }



            }
            else
            {

                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }

        private void BtnPikear_Clicked(object sender, EventArgs e)
        {
            string fecha = FConsolidado.Date.Year + "-" + FConsolidado.Date.Month + "-" + FConsolidado.Date.Day;
            string Depa = cboDpto.SelectedItem.ToString();
            App.Fconsoli = fecha;
            App.DptoConsolidado = Depa;
            Navigation.PushAsync(new SMM_Picking_Consolidado());
        }
        protected override bool OnBackButtonPressed()
        {
            //return true to prevent back, return false to just do something before going back. 
            return true;
        }
    }
}