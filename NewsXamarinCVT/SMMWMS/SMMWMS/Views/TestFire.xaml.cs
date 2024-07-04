using SMMWMS.Datos;
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
    public partial class TestFire : ContentPage
    {
        public TestFire()
        {
            InitializeComponent();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();
            await firebaseHelper.AddCar(Convert.ToInt32(txtId.Text), txtName.Text);
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            await DisplayAlert("Success", "Person Added Successfully", "OK");      
        }

        private async void BtnRetrive_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();
            var person = await firebaseHelper.GetCars(Convert.ToInt32(txtId.Text));
            if (person != null)
            {
                txtId.Text = person.carid.ToString();
                txtName.Text = person.carName;
                await DisplayAlert("Success", "Person Retrive Successfully", "OK");

            }
            else
            {
                await DisplayAlert("Success", "No Person Available", "OK");
            }
        }
    }
}