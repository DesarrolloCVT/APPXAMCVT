using SMMWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMMWMS.Datos;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SMMWMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMain : MasterDetailPage
    {
        public PageMain()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            myPageMain();
        }
        public void myPageMain()
        {
            Detail = new NavigationPage(new PageBienvenida() { Title = "Menú" });

            DatosApp dpp = new DatosApp();

            List<MenuClass> mn = dpp.TraeMenu(App.idPerfil);


            List<Option> options = new List<Option>
            {
                new Option{ page=new PageBienvenida(){Title="Menú"},title="Inicio", detail="Inicio" /*image = "ic_ac_home.png" */},
                new Option{ page=new SMM_TomaInventario(){Title="Menú"},title="Toma Inventario Mayorista", detail="Toma Inventario" },
               
                //new Option{ page=new Repaletizado(){Title="Menú"},title="Repaletizado", detail="Repaletizado"},
                //new Option{ page=new Posicionamiento(){Title="Menú"},title="Posicionamiento", detail="Posicionamiento Pallets"},
                //new Option{ page=new TomaInventario(){Title="Menú"},title="Inventario", detail="Toma Inventario"},
                //new Option{ page=new TrazabilidadPallet(){Title="Menú"},title="Trazabilidad Pallet", detail="Trazabilidad pallet"},
                //new Option{ page=new InformeStock(){Title="Menú"},title="FEFO", detail="Informe Stock"},
                //new Option{ page=new SMM_TomaInventario(){Title="Menú"},title="Toma Inventario Mayorista", detail="Inventario Supermercado Mayorista"}, 
                ////new Option{ page=new PageAbout(),title="Acerca de", detail="App DIAbetes", image = "ic_ac_about.png" },
                //new Option{ page=null,title="Cerrar Sesión", detail="Abandonar App" }
            };

            List<Option> options1 = new List<Option>();

            foreach (var t in options)
            {
                foreach (var tr in mn)
                {
                    if (tr.TituloMenu.Equals(t.title)) {

                        options1.Add(t);
                    }
                }
            }
            options1.Add(new Option { page = new TestFire() { Title = "Menú" }, title = "test", detail = "Toma Inventario" });
            options1.Add(new Option { page = null, title = "Cerrar Sesión", detail = "Abandonar App" });
            listPageMain.ItemsSource = options1;
            lblNombre.Text = "Bienvenido " + App.NombreUsuario.ToString();

            
        }
        private async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var option = e.SelectedItem as Option;
            if (option.page != null)
            {
                IsPresented = false;
                Detail = new NavigationPage(option.page);
            }
            else if (option.page == null || option.title == "Cerrar Sesión")
            {

                var result = await DisplayAlert("Confirmar", "Estas seguro de cerrar sesión", "SI", "NO");
                if (result)
                {
                    await Navigation.PopToRootAsync();

                }
                else
                {
                    await Navigation.PushAsync(new PageMain());
                }
            }
        }
    }
}