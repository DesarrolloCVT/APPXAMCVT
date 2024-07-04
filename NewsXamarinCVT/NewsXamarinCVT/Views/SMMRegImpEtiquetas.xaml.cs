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
    public partial class SMMRegImpEtiquetas : ContentPage
    {
        string v_CodigoProducto = "";

        public SMMRegImpEtiquetas()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {

            base.OnAppearing();
            txt_pallet.Text = string.Empty;
            txt_pallet.Focus();
            btn_agregar.IsEnabled = false;

        }
        private async void txt_pallet_Completed(object sender, EventArgs e)
        {
            DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                string codpro = dti.ValidaCodProducto(txt_pallet.Text);
            
                if (txt_pallet.Text.Equals(string.Empty))
                {
                    lblError2.Text = "ingrese Codigo Producto";
                    lblError2.IsVisible = true;
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    txt_pallet.Focus();
                }
                else if (codpro.Equals(""))
                {
                    lblError2.IsVisible = true;
                    await DisplayAlert("Alerta", "Codigo Producto no existe", "Aceptar");
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    txt_pallet.Text = string.Empty;
                    txt_pallet.Focus();

                }
                else
                {
                    string codiPro = dti.TraeCodProducti(txt_pallet.Text);
                    List<SMMDatoProductosRecepcion> ls = dti.ListaDatosProdRes(codiPro, txt_pallet.Text);

                    string Umd = "";
                foreach (var t in ls)
                    {
                      //  Umd = t.UomCode;
                        v_CodigoProducto = t.ItemCode;
                    }

                    lblProducto.Text = codpro.ToString();
                    lblError2.Text = string.Empty;
                    lblError2.IsVisible = false;
                    btn_agregar.IsEnabled = true;
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }

        private void btn_agregar_Clicked(object sender, EventArgs e)
        {
            if (txt_pallet.Text.Equals(string.Empty))
            {
                lblError2.Text = "Ingrese codigo";
                lblError2.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_pallet.Focus();
            }
            else
            {

                var ACC = Connectivity.NetworkAccess;
                if (ACC == NetworkAccess.Internet)
                {

                    DatosSMM_TomaInventario dTi = new DatosSMM_TomaInventario();
                    string codPro = dTi.TraeCodProducti(txt_pallet.Text);
                    if (dTi.ValidaCodProducto(txt_pallet.Text) != "")
                    {
                         try
                            {
                                DatosSMM_Etiquetas sm = new DatosSMM_Etiquetas();
                                 string rest = sm.insertaRegEtiPrec(codPro);
                                if (rest.Equals("0"))
                                {
                                    DisplayAlert("Alerta", "Registrado", "Aceptar");
                                    txt_pallet.Text = string.Empty;
                                    txt_pallet.Focus();                                  
                                    lblProducto.Text = string.Empty;                                  
                                    btn_agregar.IsEnabled = false;
                                }
                                else
                                {
                                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                    DisplayAlert("Alerta", "ERROR AL REGISTRAR,CONTACTAR CON ADMINISTRADOR", "Aceptar");
                                }

                            }
                            catch { }
                      
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        DisplayAlert("Alerta", "DUN 14 NO ENCONTRADO", "Aceptar");
                        txt_pallet.Focus();
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