using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SMMWMS
{
    public partial class App : Application
    {
        public static int Iduser { get; set; }
        public static string UserSistema { get; set; }
        public static string NombreUsuario { get; set; }
        public static int idPerfil { get; set; }    
        
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
