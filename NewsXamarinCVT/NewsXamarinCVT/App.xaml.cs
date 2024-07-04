using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NewsXamarinCVT.Views;
using Xamarin.Essentials;

namespace NewsXamarinCVT
{
    public partial class App : Application
    {
        public App()
        {

            DevExpress.XamarinForms.DataGrid.Initializer.Init();
            //DevExpress.XamarinForms.Popup.Initializer.Init();
            //DevExpress.XamarinForms.Editors.Initializer.Init();
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
          
            //MainPage = new NavigationPage(new PageMain());
        }

        public static int Iduser { get; set; }
        public static string UserSistema { get; set; }
        public static string NombreUsuario { get; set; }
        public static int idPerfil { get; set; }
        public static bool vali { get; set; }
        public static string Fconsoli { get; set; }
        public static string DptoConsolidado { get; set; }

        public static string lati { get; set; }
        public static string longi { get; set; }

        public static string altit { get; set; }
          

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
           //InitializeComponent();
            //MainPage = new NavigationPage(new PageMain());
        }

        protected override void OnResume()
        {
        //    if (vali == true) {

        //        MainPage = new NavigationPage(new PageMain());
        //    }

            
            // MainPage = new NavigationPage(new PageMain());
        }
    }
}
