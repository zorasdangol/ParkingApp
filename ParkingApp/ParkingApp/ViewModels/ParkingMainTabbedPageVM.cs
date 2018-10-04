using ParkingApp.DataAccess;
using ParkingApp.Interfaces;
using ParkingApp.SQLiteAccess;
using ParkingApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ParkingApp.ViewModels
{
    public class ParkingMainTabbedPageVM : BaseViewModel
    {
        public Command LogOutCommand { get; set; }

        public ParkingMainTabbedPageVM()
        {
            LogOutCommand = new Command(ExecuteLogOutCommand);
        }

        public async void ExecuteLogOutCommand()
        {
            try
            {
                var res = await App.Current.MainPage.DisplayAlert("Confirm", "Are you sure to Logout?", "Yes", "No");

                if (res)
                {
                    UserAccess.DeleteUserAndIP(App.DatabaseLocation);
                    App.Current.MainPage = (new LoginPage());
                }
            }catch(Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert(ex.Message);
            }
        }
    }
}
