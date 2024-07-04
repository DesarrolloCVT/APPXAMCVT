using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http.Formatting;
using NewsXamarinCVT.Views;
using NewsXamarinCVT.Models;
using Xamarin.Essentials;
using System.Text.RegularExpressions;
using Acr.UserDialogs;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
//using DevExpress.XamarinForms.Editors.Internal;
//using static Xamarin.Essentials.Permissions;

namespace NewsXamarinCVT
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
            txtUsuario.Focus();


        }
        protected override async void OnAppearing()
        {

            base.OnAppearing();
            loging.IsEnabled = true;
            txtUsuario.Focus();

        }


        private async void Loging_ClickedAsync(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Verificando Datos"))
            {
                await Task.Delay(10);
                var ssid = DependencyService.Get<IGetSSID>().GetSSID();
                string sd = Regex.Replace(ssid, @"[^\w\d]", string.Empty);

                loging.IsEnabled = false;


                //UserDialogs.Instance.HideLoading();

                var ACC = Connectivity.NetworkAccess;
                if (ACC == NetworkAccess.Internet)
                {
                    if (DeviceInfo.Model=="MC33")
                    { }

                    //bool Version = await DependencyService.Get<ILatest>().IsUsingLatestVersion();
                    //if (Version == false)
                    //{
                    //    var res = await DisplayAlert("Message", "Nueva version Disponible", "Update", "Cancel");
                    //    if (res)
                    //    {

                    //        await DependencyService.Get<ILatest>().OpenAppInStore();
                    //    }
                    //    else
                    //    {
                    //        await Navigation.PopToRootAsync();

                    //    }
                    //}
                    //else
                    //{
                        string usuario = txtUsuario.Text.ToLower();
                        string clave = txtContraseña.Text.ToLower();
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
                                try
                                {
                                    DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                                    var rest2 = ClientHttp.GetAsync("api/Usuario?idUser=" + listado).Result;
                                    var resultadoStr2 = rest2.Content.ReadAsStringAsync().Result;
                                    List<UsuarioClass> du = JsonConvert.DeserializeObject<List<UsuarioClass>>(resultadoStr2);

                                    //if (du.Count != 0) { }
                                    foreach (var d in du)
                                    {
                                        App.Iduser = listado;
                                        App.UserSistema = d.UsuarioSistema;
                                        App.NombreUsuario = d.NombreUsuario.ToString();
                                        App.idPerfil = d.IdPerfilMovile;
                                        // App.vali = true;
                                    }

                                    await Navigation.PushAsync(new PageMain());
                                    txtUsuario.Text = string.Empty;
                                    txtContraseña.Text = string.Empty;
                                }
                                catch
                                {
                                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                    await DisplayAlert("Alerta", "No tiene los perfiles necesarios para poder acceder a esta APP", "OK");
                                    txtUsuario.Text = string.Empty;
                                    txtContraseña.Text = string.Empty;
                                    txtUsuario.Focus();
                                    loging.IsEnabled = true;
                                }

                            }

                        }
                        else
                        {
                            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                            //PopAlert.IsOpen = true;
                            //lblPop.Text=  "Usuario o Contraseña No Existen ";

                            await DisplayAlert("Alerta", "Usuario o Contraseña No Existen ", "Aceptar");

                            txtUsuario.Text = string.Empty;
                            txtContraseña.Text = string.Empty;
                            txtUsuario.Focus();
                            loging.IsEnabled = true;


                        }
                    //}

                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
                    loging.IsEnabled = true;


                }
            }

        }
        #region validaciones
        //private async Task validarFormulario()
        //{
        //    //Valida si el valor en el Entry se encuentra vacio o es igual a Null
        //    if (String.IsNullOrWhiteSpace(UserName.Text))
        //    {
        //        await this.DisplayAlert("Advertencia", "El campo del nombre es obligatorio.", "OK");
        //        return false;
        //    }
        //    //Valida que solo se ingresen letras
        //    else if (!UserName.Text.ToCharArray().All(Char.IsLetter))
        //    {
        //        await this.DisplayAlert("Advertencia", "Tu información contiene números, favor de validar.", "OK");
        //        return false;
        //    }
        //    else
        //    {
        //        //Valida si se ingresan caracteres especiales
        //        string caractEspecial = @"^[^ ][a-zA-Z ]+[^ ]$";
        //        bool resultado = Regex.IsMatch(UserName.Text, caractEspecial, RegexOptions.IgnoreCase);
        //        if (!resultado)
        //        {
        //            await this.DisplayAlert("Advertencia", "No se aceptan caracteres especiales, intente de nuevo.", "OK");
        //            return false;
        //        }
        //    }

        //    if (String.IsNullOrWhiteSpace(UserLastName.Text))
        //    {
        //        await this.DisplayAlert("Advertencia", "El campo del apellido es obligatorio.", "OK");
        //        return false;
        //    }
        //    //Valida que solo se ingresen letras
        //    else if (!UserLastName.Text.ToCharArray().All(Char.IsLetter))
        //    {
        //        await this.DisplayAlert("Advertencia", "Tu información contiene números, favor de validar.", "OK");
        //        return false;
        //    }
        //    else
        //    {
        //        //Valida si se ingresan caracteres especiales
        //        string caractEspecial = @"^[^ ][a-zA-Z ]+[^ ]$";
        //        bool resultado = Regex.IsMatch(UserLastName.Text, caractEspecial, RegexOptions.IgnoreCase);
        //        if (!resultado)
        //        {
        //            await this.DisplayAlert("Advertencia", "No se aceptan caracteres especiales, intente de nuevo.", "OK");
        //            return false;
        //        }
        //    }

        //    if (String.IsNullOrWhiteSpace(UserEmail.Text))
        //    {
        //        await this.DisplayAlert("Advertencia", "El campo del correo electronico es obligatorio.", "OK");
        //        return false;
        //    }
        //    else
        //    {
        //        //Valida que el formato del correo sea valido
        //        bool isEmail = Regex.IsMatch(UserEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        //        if (!isEmail)
        //        {
        //            await this.DisplayAlert("Advertencia", "El formato del correo electrónico es incorrecto, revíselo e intente de nuevo.", "OK");
        //            return false;
        //        }
        //    }

        //    if (String.IsNullOrWhiteSpace(UserCelular.Text))
        //    {
        //        await this.DisplayAlert("Advertencia", "El campo del número celular es obligatorio.", "OK");
        //        return false;
        //    }
        //    //Valida si la cantidad de digitos ingresados es menor a 10
        //    else if (UserCelular.Text.Length != 10)
        //    {
        //        await this.DisplayAlert("Advertencia", "Faltan digitos, favor de ingresar su numero a 10 digitos.", "OK");
        //        return false;
        //    }
        //    else
        //    {
        //        //Valida que solo se ingresen numeros
        //        if (!UserCelular.Text.ToCharArray().All(Char.IsDigit))
        //        {
        //            await this.DisplayAlert("Advertencia", "El formato del celular es incorrecto, solo se aceptan numeros.", "OK");
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        #endregion



        private void txtUsuario_Completed_1(object sender, EventArgs e)
        {
            //txtContraseña.Focus();
        }

        private void txtContraseña_Completed(object sender, EventArgs e)
        {
            //loging.Focus();
        }

        //private async void BtnIniHuella_ClickedAsync(object sender, EventArgs e)
        //{
        //    //verificar si consta con lector de huellas
        //    var res = await CrossFingerprint.Current.IsAvailableAsync(true);
        //    var cancellation = new System.Threading.CancellationToken();

        //    if (res)
        //    {
        //        var request = new AuthenticationRequestConfiguration("Prove you have fingers!", "Because without it you can't have access");
        //        var result = await CrossFingerprint.Current.AuthenticateAsync(request);
        //        if (result.Authenticated)
        //        {
        //            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        //        }
        //        else
        //        {
        //            await DisplayAlert("Alerta", "Mierda", "Aceptar");
        //        }
        //    }
        //    else
        //    {
        //        await DisplayAlert("Oops", "Dispositivo no compatible.", "OK");
        //    }

        //}
    }
}
