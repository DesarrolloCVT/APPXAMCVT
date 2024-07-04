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
    public partial class SMM_ConsultaProducto : ContentPage
    {
        public SMM_ConsultaProducto()
        {
            InitializeComponent();
            lblError2.Text = string.Empty;
            lblError2.IsVisible = false;
            txt_pallet.Focus();
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            txt_pallet.Focus();
            lblError2.Text = string.Empty;
            lblError2.IsVisible = false;         
            txt_pallet.Text = string.Empty;
            lblProducto.Text = string.Empty;
            lblPrecio.Text = string.Empty;
            lblUM.Text = string.Empty;
            lblCantUM.Text = string.Empty;
            lblGrupArt.Text = string.Empty;
            lblEstado.Text = string.Empty;
        }
        private void Txt_pallet_Completed(object sender, EventArgs e)
        {

            DatosProductosSMM dti = new DatosProductosSMM();
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                string codpro = dti.ValidaProductoSMM(txt_pallet.Text);
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
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");

                }
                else
                {

                    List<ValidadorProductosSMMClass> ls = dti.ListDatoProductosSMMValida(txt_pallet.Text);
                    foreach (var t in ls)
                    {
                        lblProducto.Text = t.ItemCode + " -  " + t.ItemName;
                        lblPrecio.Text = "Precio: " + t.Price;
                        lblUM.Text = "Uni.Medida: " + t.UM;
                        lblCantUM.Text = "Cant.xUM: " + t.CantxUM;
                        lblGrupArt.Text = "Grup. Articulo: " + t.GrupoArticulo;
                        lblUnidades.Text = "Stock unidades Sala:" + t.StockUnidadesSala;
                        

                        if (t.Estado.Equals("Activo"))
                        {
                            lblEstado.TextColor = Color.Blue;
                            lblEstado.Text = "Estado :" + t.Estado;

                        }
                        else {
                            lblEstado.TextColor = Color.Red;
                            lblEstado.Text = "Estado :" + t.Estado;
                        }
                        

                    }
                    
                    //lblProducto.Text = codpro.ToString();
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

        private void Btn_Limpiar_Clicked(object sender, EventArgs e)
        {
            lblError2.Text = string.Empty;
            lblError2.IsVisible = false;
            txt_pallet.Text = string.Empty;
            lblProducto.Text = string.Empty;
            lblPrecio.Text = string.Empty;
            lblUM.Text = string.Empty;
            lblCantUM.Text = string.Empty;
            lblGrupArt.Text = string.Empty;
            lblEstado.Text = string.Empty;
            lblUnidades.Text = string.Empty;
            txt_pallet.Focus();
        }
    }
}