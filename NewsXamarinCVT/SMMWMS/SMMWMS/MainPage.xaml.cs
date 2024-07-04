using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using SMMWMS.Models;
using SMMWMS.Views;

namespace SMMWMS
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            loging.IsEnabled = true;
            activity.IsRunning = false;

        }


        private async void Loging_ClickedAsync(object sender, EventArgs e)
        {
            var prof = Connectivity.ConnectionProfiles;

            loging.IsEnabled = false;
            var ACC = Connectivity.NetworkAccess;
            //&& prof.Contains(ConnectionProfile.WiFi)
            if (ACC == NetworkAccess.Internet )
            {

                bool Version = await DependencyService.Get<ILatest>().IsUsingLatestVersion();
                if (Version == false)
                {                   
                    var res = await DisplayAlert("Message", "Nueva version Disponible", "Update", "Cancel");
                    if (res)
                    {
                        await DependencyService.Get<ILatest>().OpenAppInStore();
                    }
                    else
                    {
                        await Navigation.PopToRootAsync();
                        loging.IsEnabled = true;
                    }
                }
                else
                {
                    activity.IsEnabled = true;
                    activity.IsRunning = true;
                    activity.IsVisible = true;

                    string usuario = txtUsuario.Text;
                    string clave = txtContraseña.Text;
                    HttpClient ClientHttp = new HttpClient();
                    ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                    //para Consultar 
                    var rest = ClientHttp.GetAsync("api/Usuario?usuario=" + usuario + "&pass=" + clave).Result;

                    if (rest.IsSuccessStatusCode)
                    {
                        var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                        var listado = JsonConvert.DeserializeObject<int>(resultadoStr);


                        if (listado != 0)
                        {
                            DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                            var rest2 = ClientHttp.GetAsync("api/Usuario?idUser=" + listado).Result;
                            var resultadoStr2 = rest2.Content.ReadAsStringAsync().Result;
                            List<UsuarioClass> du = JsonConvert.DeserializeObject<List<UsuarioClass>>(resultadoStr2);

                            foreach (var d in du)
                            {
                                App.Iduser = listado;
                                App.UserSistema = d.UsuarioSistema;
                                App.NombreUsuario = d.NombreUsuario.ToString();
                                App.idPerfil = d.IdPerfil;
                            }

                            await Navigation.PushAsync(new PageMain());
                            txtUsuario.Text = string.Empty;
                            txtContraseña.Text = string.Empty;
                        }


                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        await DisplayAlert("Alerta", "Usuario o Contraseña No Existen ", "Aceptar");
                        txtUsuario.Text = string.Empty;
                        txtContraseña.Text = string.Empty;
                        txtUsuario.Focus();
                        activity.IsRunning = false;
                        loging.IsEnabled = true;


                    }
                }

            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
                loging.IsEnabled = true;


            }


        }

        private void TxtUsuario_Completed(object sender, EventArgs e)
        {
            txtContraseña.Focus();
        }
    }
}
