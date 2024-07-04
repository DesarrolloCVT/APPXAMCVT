using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SMMWMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBienvenida : ContentPage
    {
        public PageBienvenida()
        {
            InitializeComponent();
            lblNombre.Text = App.NombreUsuario.ToString();
        }
        protected override bool OnBackButtonPressed()
        {
            //return true to prevent back, return false to just do something before going back. 
            return true;
        }
    }
}