using Newtonsoft.Json;
using ParkingApp.DataAccess;
using ParkingApp.DataValidation;
using ParkingApp.Interfaces;
using ParkingApp.SQLiteAccess;
using ParkingApp.Views;
using ParkingCommonLibrary.Helpers;
using ParkingCommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ParkingApp.ViewModels
{
    public class LoginPageVM:BaseViewModel
    {
        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { _IsLoading = value; OnPropertyChanged("IsLoading"); }
        }

        private string _ip1;
        public string ip1
        {
            get { return _ip1; }
            set
            {
                _ip1 = value;
                OnPropertyChanged("ip1");
            }
        }

        private string _ip2;
        public string ip2
        {
            get { return _ip2; }
            set
            {
                _ip2 = value;
                OnPropertyChanged("ip2");
            }
        }

        private string _ip3;
        public string ip3
        {
            get { return _ip3; }
            set
            {
                _ip3 = value;
                OnPropertyChanged("ip3");
            }
        }
        private string _ip4;
        public string ip4
        {
            get { return _ip4; }
            set
            {
                _ip4 = value;
                OnPropertyChanged("ip4");
            }
        }

        private string _Port;
        public string Port
        {
            get { return _Port; }
            set
            {
                _Port = value;
                OnPropertyChanged("Port");
            }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged("Password");
            }
        }        

        private bool _HidePassword;
        public bool HidePassword
        {
            get { return _HidePassword; }
            set
            {
                _HidePassword = value;
                OnPropertyChanged("HidePassword");
            }
        }

        public User User { get; set; }

        public Command LoginCommand { get; set; }

        public LoginPageVM()
        {
            var user = ParkingCommonLibrary.Helpers.GlobalClass.User;
            UserName = user.UserName;
            Password = "";
            //Password = user.Password;
            ip1 = user.ip1;
            ip2 = user.ip2;
            ip3 = user.ip3;
            ip4 = user.ip4;
            Port =user.Port;

            HidePassword = true;

            LoginCommand = new Command(ExecuteLoginCommand);
            User = new User();
            IsLoading = false;

        }

        public async void ExecuteLoginCommand()
        {
            try
            {
                IsLoading = true;
                User.UserName = UserName;
                User.Password = Password;
                User.ip1 = ip1;
                User.ip2 = ip2;
                User.ip3 = ip3;
                User.ip4 = ip4;
                User.Port = Port;

                Helpers.Constants.SetMainURL(User);
                var functionResponse = UserValidator.CheckUser(User);
                if (functionResponse.status == "error")
                {
                    DependencyService.Get<IMessage>().ShortAlert(functionResponse.Message);
                }
                else
                {
                    var functionRes = await LoginConnection.UserVerficationAsync(User);
                    if (functionRes.status == "ok")
                    {
                        var user  = JsonConvert.DeserializeObject<User>(functionRes.result.ToString());
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
                        ParkingCommonLibrary.Helpers.GlobalClass.User = user;
                        Helpers.Constants.SetMainURL(user);
                        UserAccess.SetUserAndIP(App.DatabaseLocation, user);

                        DependencyService.Get<IMessage>().ShortAlert("Logged In Successfully");
                        App.Current.MainPage = new NavigationPage(new ParkingMainTabbedPage());                       
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert(functionRes.Message);
                    }
                    IsLoading = false;
                }
            }
            catch (Exception e)
            {
                IsLoading = false;
                DependencyService.Get<IMessage>().ShortAlert("Error::" + e.Message);
            }
        }

    }
}

