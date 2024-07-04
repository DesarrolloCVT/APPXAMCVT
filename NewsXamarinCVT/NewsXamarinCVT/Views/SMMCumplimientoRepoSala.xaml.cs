﻿using Acr.UserDialogs;
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
    public partial class SMMCumplimientoRepoSala : ContentPage
    {
        public SMMCumplimientoRepoSala()
        {
            InitializeComponent();
            cargaDatos();
            txtCodigo.Focus();
            GvGridDatos.IsVisible = false;
          
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
         
            lblProducto.Text = string.Empty;
            lblError2.Text = string.Empty;                
            cboNombreRepo.SelectedIndex = -1;
            txtDia.Text = string.Empty;
            txtMes.Text = string.Empty;
            txtAno.Text = string.Empty;
            GvGridDatos.IsVisible = false;
            cboDispo.SelectedIndex = -1;
            cboLimpieza.SelectedIndex = -1;
            CboFefo.SelectedIndex = -1;
            CboFleje.SelectedIndex = -1;             
            btn_agregar.IsEnabled = false;
            txtCodigo.Focus();
            //lblError.Text = string.Empty;
            //lblError.IsVisible = false;

            //lblError2.IsVisible = false;
            //lblError3.Text = string.Empty;
            //lblError3.IsVisible = false;
            ////txt_Bodega.SelectedIndex = -1;

            //txt_pallet.Text = string.Empty;

            //txt_cantidad.Text = string.Empty;

        }

        void cargaDatos()
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                DatosCumplimientoRepoSalaSMM ti = new DatosCumplimientoRepoSalaSMM();
                List<SMMReponedoresClass> lt = ti.ListaReponedores();           

                List<SMMReponedoresClass> fl = new List<SMMReponedoresClass>();          

                foreach (var t in lt)
                {
                    fl.Add(new SMMReponedoresClass { IdReponedor =t.IdReponedor, Nombre = t.Nombre });

                }
                cboNombreRepo.BindingContext = fl;              

            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }

            txtCodigo.Focus();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                string codpro = dti.ValidaCodProducto(txtCodigo.Text);
                if (txtCodigo.Text.Equals(string.Empty))
                {
                    lblError2.Text = "ingrese Codigo Producto";
                    lblError2.IsVisible = true;
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    //txt_pallet.Focus();
                }
                else if (codpro.Equals(""))
                {
                    lblError2.IsVisible = true;
                    await DisplayAlert("Alerta", "Codigo Producto no existe", "Aceptar");
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    txtCodigo.Text = string.Empty;
                    txtCodigo.Focus();

                }
                else
                {

                    // Fvenci.Focus();
                    //txtDia.Focus();
                    string codiPro = dti.TraeCodProducti(txtCodigo.Text);
                    List<SMMDatoProductosRecepcion> ls = dti.ListaDatosProdRes(codiPro, txtCodigo.Text);

                    string Umd = "";
                    foreach (var t in ls)
                    {
                        Umd = t.UomCode;
                    }

                    lblProducto.Text = codpro.ToString() + " //  " + Umd.ToString();
                    lblError2.Text = string.Empty;
                    lblError2.IsVisible = false;
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }

        private async void txtCodigo_Completed(object sender, EventArgs e)
        {
            DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                string codpro = dti.ValidaCodProducto(txtCodigo.Text);
                if (txtCodigo.Text.Equals(string.Empty))
                {
                    lblError2.Text = "ingrese Codigo Producto";
                    lblError2.IsVisible = true;
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    txtCodigo.Focus();
                }
                else if (codpro.Equals(""))
                {
                    lblError2.IsVisible = true;
                    await DisplayAlert("Alerta", "Codigo Producto no existe", "Aceptar");
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    txtCodigo.Text = string.Empty;
                    txtCodigo.Focus();

                }
                else
                {

                    // Fvenci.Focus();
                    //txtDia.Focus();
                    string codiPro = dti.TraeCodProducti(txtCodigo.Text);
                    List<SMMDatoProductosRecepcion> ls = dti.ListaDatosProdRes(codiPro, txtCodigo.Text);

                    string Umd = "";
                    foreach (var t in ls)
                    {
                        Umd = t.UomCode;
                    }

                    lblProducto.Text = codpro.ToString() + " //  " + Umd.ToString();
                    lblError2.Text = string.Empty;
                    lblError2.IsVisible = false;
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }

        private void cboNombreRepo_SelectionChanged(object sender, EventArgs e)
        {
            txtDia.Focus();
        }

        private async void txtDia_Completed(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtDia.Text) > 31 || (txtDia.Text.Equals("") || Convert.ToInt32(txtDia.Text) == 0))
            {
                using (UserDialogs.Instance.Alert("ingrese dia correcto"))
                {
                    await Task.Delay(5);


                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "ingrese dia", "Aceptar");
                }
                txtDia.Focus();
            }
            else { txtMes.Focus(); }
        }

        private async void txtMes_Completed(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtMes.Text) > 12 || (txtMes.Text.Equals("") || Convert.ToInt32(txtMes.Text) == 0))
            {
                using (UserDialogs.Instance.Alert("ingrese mes correcto"))
                {
                    await Task.Delay(5);


                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "ingrese mes correcto", "Aceptar");
                }
                txtMes.Focus();
            }
            else {
                txtAno.Focus();
            
            }
        }

        private async void txtAno_Completed(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtAno.Text) < DateTime.Now.Year || txtAno.Text.Equals(""))
            {
                using (UserDialogs.Instance.Alert("ingrese año correcto"))
                {
                    await Task.Delay(5);


                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "ingrese año correcto", "Aceptar");
                   
                }
                txtAno.Focus();
            }
            else {
                GvGridDatos.IsVisible = true;
                btn_agregar.IsEnabled = true;               
            }
        }

        private void cboDispo_SelectionChanged(object sender, EventArgs e)
        {
            //cboLimpieza.Focus();
        }

        private void cboLimpieza_SelectionChanged(object sender, EventArgs e)
        {
            //CboFefo.Focus();
        }

        private void CboFefo_SelectionChanged(object sender, EventArgs e)
        {
            //CboFleje.Focus();
        }

        private void btn_agregar_Clicked(object sender, EventArgs e)
        {
            try {
                if (txtCodigo.Text.Equals(string.Empty) || txtCodigo.Equals(null))
                {
                    txtCodigo.Focus();
                    lblError2.Text = "ingrese Codigo Producto";
                    lblError2.IsVisible = true;
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");

                }
                if(cboNombreRepo.SelectedIndex==-1)
                {
                    cboNombreRepo.Focus();
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    lblError2.Text = "Seleccione Reponedor";
                    lblError2.IsVisible = true;
                }
                else
                {
                    var ACC = Connectivity.NetworkAccess;
                    if (ACC == NetworkAccess.Internet)
                    {
                        DatosSMM_TomaInventario dTi = new DatosSMM_TomaInventario();
                        string codPro = dTi.TraeCodProducti(txtCodigo.Text);

                        if (dTi.ValidaCodProducto(txtCodigo.Text) != "")
                        {
                            try
                            {                                

                                int v_idNVerificado = Convert.ToInt32(cboNombreRepo.SelectedValue);
                                int v_Verificador = App.Iduser;
                                string v_CodProd = codPro;
                                string v_CodBar = txtCodigo.Text;
                                string v_dispo =cboDispo.SelectedIndex==-1?"0":cboDispo.SelectedValue.ToString();
                                string v_limp = cboLimpieza.SelectedIndex == -1 ? "0" : cboLimpieza.SelectedValue.ToString();
                                string v_fefo = CboFefo.SelectedIndex == -1 ? "0" : CboFefo.SelectedValue.ToString();
                                string v_fleje = CboFleje.SelectedIndex == -1 ? "0" : CboFleje.SelectedValue.ToString();
                                string v_fechVenc = txtAno.Text + "-" + txtMes.Text + "-" + txtDia.Text;

                                DatosCumplimientoRepoSalaSMM cum= new DatosCumplimientoRepoSalaSMM();


                                string rest = cum.insertaRegistroCumplimiento(v_idNVerificado, v_Verificador, v_CodProd, v_CodBar, v_dispo, v_limp, v_fefo, v_fleje, v_fechVenc);
                                if (rest.Equals("0"))
                                {
                                    DisplayAlert("Alerta", "Registrado", "Aceptar");
                                    txtCodigo.Text = string.Empty;
                                    txtCodigo.Focus();
                                    GvGridDatos.IsVisible =false;
                                    lblProducto.Text = string.Empty;
                                    txtDia.Text = string.Empty;
                                    txtMes.Text = string.Empty;
                                    txtAno.Text = string.Empty;
                                    btn_agregar.IsEnabled = false;
                                    cboNombreRepo.SelectedIndex = -1;
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
                            txtCodigo.Focus();
                        }
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
                    }

                }
                
            }
            catch(Exception ex)
            {
                lblError2.IsVisible = true;
                lblError2.Text = ex.Message+" "+"contactar al administrador";
                lblError2.Focus();
            }
            txtCodigo.Focus();
        }

        protected override bool OnBackButtonPressed()
        {
            //return true to prevent back, return false to just do something before going back. 
            return true;
        }

    }
}