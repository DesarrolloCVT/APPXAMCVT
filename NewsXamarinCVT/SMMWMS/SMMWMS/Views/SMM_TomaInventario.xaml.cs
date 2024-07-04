using SMMWMS.Datos;
using SMMWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SMMWMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SMM_TomaInventario : ContentPage
    {
        public SMM_TomaInventario()
        {
            InitializeComponent();
            cargaDatos();
            lblError.Text = string.Empty;
            lblError.IsVisible = false;
            lblError2.Text = string.Empty;
            lblError2.IsVisible = false;
            lblError3.Text = string.Empty;
            lblError3.IsVisible = false;
        }
        protected override async void OnAppearing()
        {

            base.OnAppearing();
            cboFolio.SelectedIndex = -1;
            cboFolio.Focus();
            cboPasillo.SelectedIndex = -1;
            btn_agregar.IsEnabled = false;
            lblError.Text = string.Empty;
            lblError.IsVisible = false;
            lblError2.Text = string.Empty;
            lblError2.IsVisible = false;
            lblError3.Text = string.Empty;
            lblError3.IsVisible = false;
            txt_Bodega.Text = string.Empty;
            txt_pallet.Text = string.Empty;
            lblProducto.Text = string.Empty;
            txt_cantidad.Text = string.Empty;

        }
        void cargaDatos()
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                DatosSMM_TomaInventario ti = new DatosSMM_TomaInventario();
                List<TomaInventarioClass> lt = ti.TraeFoliosInventarios();

                foreach (var t in lt)
                {
                    cboFolio.Items.Add(t.Inventario_Id.ToString());

                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }

        private void CboFolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Bodega.Focus();
        }

        private void Txt_Bodega_Completed(object sender, EventArgs e)
        {
            DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                if (txt_Bodega.Text.Equals(string.Empty))
                {
                    lblError.Text = "ingrese Bodega";
                    lblError.IsVisible = true;
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    txt_Bodega.Focus();
                }
                else if (Convert.ToInt32(dti.ValidaBodegaSMM(txt_Bodega.Text)) == 0)
                {
                    lblError.IsVisible = true;
                    txt_Bodega.Focus();
                    lblError.Text = "Bodega no existe";
                    txt_Bodega.Text = string.Empty;
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");

                }
                else
                {

                    cboPasillo.Focus();
                    lblError.Text = string.Empty;
                    lblError.IsVisible = false;
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }


        }

        private void CboPasillo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_pallet.Focus();
        }

        private void Txt_pallet_Completed(object sender, EventArgs e)
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
                    txt_pallet.Focus();
                    lblError2.Text = "Cod.Prodcuto No Existe";
                    txt_pallet.Text = string.Empty;
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");

                }
                else
                {

                    txt_cantidad.Focus();
                    lblProducto.Text = codpro.ToString();
                    lblError2.Text = string.Empty;
                    lblError2.IsVisible = false;
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }

        private void Txt_cantidad_Completed(object sender, EventArgs e)
        {
            if (txt_cantidad.Text.Equals(string.Empty))
            {
                lblError3.Text = "Ingrese Cantidad";
                lblError3.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                //DisplayAlert("Alerta",, "Aceptar");          
                txt_cantidad.Focus();
            }
            else if (!txt_cantidad.Text.ToCharArray().All(Char.IsDigit))
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                lblError3.Text = "Ingrese solo Numeros";
                lblError3.IsVisible = true;
                txt_cantidad.Focus();
                txt_cantidad.Text = string.Empty;
            }
            else
            {
                btn_agregar.IsEnabled = true;
                lblError3.Text = string.Empty;
                lblError3.IsVisible = false;
            }
        }

        private void Btn_agregar_Clicked(object sender, EventArgs e)
        {
            if (cboFolio.SelectedIndex == -1)
            {
                cboFolio.Focus();
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            }
            else if (txt_Bodega.Text.Equals(string.Empty))
            {
                lblError.Text = "ingrese Bodega";
                lblError.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_Bodega.Focus();
            }
            else if (cboPasillo.SelectedIndex == -1)
            {
                cboPasillo.Focus();
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            }
            else if (txt_pallet.Text.Equals(string.Empty))
            {
                lblError2.Text = "ingrese Codigo Producto";
                lblError2.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_pallet.Focus();
            }
            else if (txt_cantidad.Text.Equals(string.Empty))
            {
                lblError3.Text = "Ingrese Cantidad";
                lblError3.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_cantidad.Focus();
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
                        if (Convert.ToInt32(dTi.ValidaBodegaSMM(txt_Bodega.Text)) != 0)
                        {
                            try
                            {

                                int idinv = Convert.ToInt32(cboFolio.SelectedItem);
                                string dn14 = txt_pallet.Text;
                                int cant = Convert.ToInt32(txt_cantidad.Text);
                                int idBod = Convert.ToInt32(dTi.ValidaBodegaSMM(txt_Bodega.Text));
                                int iduser = App.Iduser;
                                string ubPasillo = cboPasillo.SelectedItem.ToString();

                                bool rest = dTi.insertaInventario(idinv, dn14, codPro, cant, idBod, iduser, ubPasillo);
                                if (rest == true)
                                {
                                    DisplayAlert("Alerta", "Registrado", "Aceptar");
                                    txt_pallet.Text = string.Empty;
                                    txt_pallet.Focus();
                                    txt_cantidad.Text = string.Empty;
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
                            DisplayAlert("Alerta", "BODEGA NO ENCONTRADA", "Aceptar");
                            txt_Bodega.Focus();

                        }
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