using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ParkingApp.DataAccess;
using ParkingApp.DataValidation;
using ParkingApp.Interfaces;
using ParkingApp.SQLiteAccess;
using ParkingApp.Views;
using ParkingCommonLibrary.Helpers;
using ParkingCommonLibrary.Models;

using Xamarin.Forms;

namespace ParkingApp
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        public App(string databaseLocation)
        {

            InitializeComponent();

            DatabaseLocation = databaseLocation;

            CheckLoggedInAsync();
        }

        private async void CheckLoggedInAsync()
        {
            var res = UserAccess.LoadUserAndIP(App.DatabaseLocation);
            if (res)
            {
                var User = Helpers.Constants.User;
                var functionResponse = UserValidator.CheckUser(User);
                if (functionResponse.status == "error")
                {
                    DependencyService.Get<IMessage>().ShortAlert(functionResponse.Message);
                }
                else
                {
                    functionResponse = await LoginConnection.UserVerficationAsync(User);
                    if (functionResponse.status == "ok")
                    {
                        var user = JsonConvert.DeserializeObject<User>(functionResponse.result.ToString());
                        user.Password = User.Password;
                        user.ip1 = User.ip1;
                        user.ip2 = User.ip2;
                        user.ip3 = User.ip3;
                        user.ip4 = User.ip4;
                        user.Port = User.Port;
                        GlobalClass.Session = user.Session;
                        GlobalClass.CompanyName = user.CompanyName;
                        GlobalClass.CompanyAddress = user.CompanyAddress;
                        GlobalClass.MemberBarcodePrefix = user.MemberBarcodePrefix;

                        Helpers.Constants.User = user;
                        GlobalClass.User = user;
                        Helpers.Constants.SetMainURL(user);
                        UserAccess.SetUserAndIP(App.DatabaseLocation, user);

                        DependencyService.Get<IMessage>().ShortAlert("Logged In Successfully");
                        App.Current.MainPage = new NavigationPage(new ParkingMainTabbedPage());
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert(functionResponse.Message);
                        MainPage = new LoginPage();
                    }

                    //MainPage = new LoginPage();

                }
            }
            else
                MainPage = new LoginPage();

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
