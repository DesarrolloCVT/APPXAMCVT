using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NewsXamarinCVT.Views;
using NewsXamarinCVT.Models;
using NewsXamarinCVT.Datos;

namespace NewsXamarinCVT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPruebaChanta :ContentPage
    {
        public MenuPruebaChanta()
        {
            
            InitializeComponent();

            btnPosicionamiento.IsVisible = false;
            bntConsultaUbicacion.IsVisible = false;
            btnRepaletizado.IsVisible = false;
            bntTomaInventario.IsVisible = false;

            lblNombre.Text = App.NombreUsuario.ToString();
            switch (App.idPerfil)
            {
                case 0 :
                    btnPosicionamiento.IsVisible = false;
                    bntConsultaUbicacion.IsVisible = false;
                    btnRepaletizado.IsVisible = false;
                    bntTomaInventario.IsVisible = false;

                    break;            
                default:
                    break;

            }

            DatosApp dpp = new DatosApp();
            List<MenuClass> mn = dpp.TraeMenu(App.idPerfil);

            List<Button> bt = new List<Button>();
            bt.Add(btnPosicionamiento);
            bt.Add(bntConsultaUbicacion);
            bt.Add(btnRepaletizado);
            bt.Add(bntTomaInventario);

            foreach (var t in bt)
            {
                foreach (var tr in mn)
                {
                    if (tr.TituloMenu.Equals(t.Text))
                    {
                        t.IsVisible = true;                        
                    }                   
                }
            }
        }


        private void BntConsultaUbicacion_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConsultaUbicacion() { Title = "Volver" });
        }

        private void BtnPosicionamiento_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Posicionamiento() { Title = "Volver" });
        }

        private void BtnRepaletizado_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Repaletizado() { Title = "Volver" });
        }

        private void BntTomaInventario_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TrazabilidadPallet() { Title = "Volver" });
        }
        protected override bool OnBackButtonPressed()
        {
            //return true to prevent back, return false to just do something before going back. 
            return true;
        }

        //private void ConsultaUbicacion_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new ConsultaUbicacion());
        //}

        //private void AgregarUsuario_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new Agregar());
        //}

        //private void DatoUsuarios_Clicked(object sender, EventArgs e)
        //{

        //}

    }
}