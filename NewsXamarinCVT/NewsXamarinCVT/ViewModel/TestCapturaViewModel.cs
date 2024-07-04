using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

using System.Runtime.CompilerServices;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace NewsXamarinCVT.ViewModel
{
    public class TestCapturaViewModel: INotifyPropertyChanged
    {
        private string _result;
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public ICommand ButtonCommand { get; private set; }
        public TestCapturaViewModel()
        {
            ButtonCommand = new Command(OnButtomCommand);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnButtomCommand()
        {
            var options = new MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<BarcodeFormat>
            {
                BarcodeFormat.QR_CODE,
                BarcodeFormat.CODE_128,
                BarcodeFormat.EAN_13,
                BarcodeFormat.CODE_39,
                BarcodeFormat.AZTEC,
                BarcodeFormat.All_1D,
                BarcodeFormat.CODE_93,
                BarcodeFormat.DATA_MATRIX,
                BarcodeFormat.EAN_8,
                BarcodeFormat.MAXICODE,
                BarcodeFormat.IMB,
                BarcodeFormat.ITF,
                BarcodeFormat.MSI,
                BarcodeFormat.PDF_417,
                BarcodeFormat.PLESSEY,
                BarcodeFormat.RSS_14,
                BarcodeFormat.UPC_EAN_EXTENSION,
            };
            var page = new ZXingScannerPage(options) { Title = "SCANNER" };
            var closeItem = new ToolbarItem { Text = "CERRAR" };
            closeItem.Clicked += (object sender, EventArgs e) =>
            {
                page.IsScanning = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.Navigation.PopModalAsync();
                });
            };
            page.ToolbarItems.Add(closeItem);
            page.OnScanResult += (result) =>
            {
                page.IsScanning = false;

                Device.BeginInvokeOnMainThread(() => {
                    Application.Current.MainPage.Navigation.PopModalAsync();
                    if (string.IsNullOrEmpty(result.Text))
                    {
                        Result = "Codigo no valido en Scanner";
                    }
                    else
                    {
                        Result = $"{result.Text}";
                    }
                });
            };
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(page) { BarTextColor = Color.White, BarBackgroundColor = Color.CadetBlue }, true);
        }
    }
}
