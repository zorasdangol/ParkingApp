using ParkingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePageVM viewModel { get; set; }
        public HomePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new HomePageVM();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(400);
            viewModel.Barcode = "";
            BarcodeEntry.Focus();
        }

        public void OnEnterPressed(object sender, EventArgs e)
        {
            viewModel.ExecuteLoad();
            viewModel.Barcode = "";
            BarcodeEntry.Focus();
        }

        public void OnStaffEnterPressed(object sender, EventArgs e)
        {
            viewModel.ExecuteStaffCommand();
        }
        
        protected override bool OnBackButtonPressed()
        {
            if (viewModel.IsLoading || viewModel.IsStaffBarcode)
            {
                viewModel.IsLoading = false;
                viewModel.IsStaffBarcode = false;
                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}