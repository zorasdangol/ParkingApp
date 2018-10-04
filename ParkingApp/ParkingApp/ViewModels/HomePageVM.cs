using Newtonsoft.Json;
using ParkingApp.DataAccess;
using ParkingApp.Interfaces;
using ParkingCommonLibrary.Helpers;
using ParkingCommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ParkingApp.ViewModels
{
    public class HomePageVM : BaseViewModel
    {
        private string _Barcode;
        public string Barcode
        {
            get { return _Barcode; }
            set
            {
                if (value == null)
                    return;
                _Barcode = value;
                OnPropertyChanged("Barcode");
            }
        }


        private string _StaffBarcode;
        public string StaffBarcode
        {
            get { return _StaffBarcode; }
            set
            {
                if (value == null)
                    return;
                _StaffBarcode = value;
                OnPropertyChanged("StaffBarcode");
            }
        }

        private BarCodeTransfer _ParkingDetails;
        public BarCodeTransfer ParkingDetails
        {
            get { return _ParkingDetails; }
            set
            {
                if (value == null)
                    return;
                _ParkingDetails = value;
                OnPropertyChanged("ParkingDetails");
            }
        }

        private ParkingIn _PIN;
        public ParkingIn PIN
        {
            get { return _PIN; }
            set
            {
                if (value == null)
                    return;
                _PIN = value;
                OnPropertyChanged("PIN");
            }
        }

        private ParkingOut _POUT;
        public ParkingOut POUT
        {
            get { return _POUT; }
            set
            {
                if (value == null)
                    return;
                _POUT = value;
                OnPropertyChanged("POUT");
            }
        }


        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { _IsLoading = value; OnPropertyChanged("IsLoading"); }
        }


        private bool _IsStaffBarcode;
        public bool IsStaffBarcode
        {
            get { return _IsStaffBarcode; }
            set { _IsStaffBarcode = value; OnPropertyChanged("IsStaffBarcode"); }
        }

        private string _LoadingMessage;
        public string LoadingMessage
        {
            get { return _LoadingMessage; }
            set { _LoadingMessage = value; OnPropertyChanged("LoadingMessage");    }
        }

        public List<Voucher> Vouchers { get; set; }
        public MemberDiscount mDiscount { get; set; }

        private string _Test;
        public string Test
        { get { return _Test; } set { _Test = value; OnPropertyChanged("Test"); } }

        public Command LoadCommand { get; set; }
        public Command FinishCommand { get; set; }
        public Command StampCommand { get; set; }
        public Command StaffCommand { get; set; }
        public Command BackCommand { get; set; }
        public Command ViewStaffBarcode { get; set; }
        
        public HomePageVM()
        {
            Test = "Test";
            IsLoading = false;
            LoadingMessage = "Loading!! Please wait.....";
            Barcode = "";
            LoadCommand = new Command(ExecuteLoad);
            FinishCommand = new Command(ExecuteFinishCommand);
            StampCommand = new Command(ExecuteStampCommand);
            StaffCommand = new Command(ExecuteStaffCommand);
            BackCommand = new Command(ExecuteBackCommand);
            ViewStaffBarcode = new Command(ExecuteViewStaffBarcode);
            PIN = new ParkingIn();
            POUT = new ParkingOut();
            ParkingDetails = new BarCodeTransfer();
            Vouchers = new List<Voucher>();
            mDiscount = new MemberDiscount();
            StaffBarcode = "";
        }

        private void ExecuteViewStaffBarcode(object obj)
        {
            if(POUT.PID == 0 || PIN.PID == 0)
            {
                DependencyService.Get<IMessage>().ShortAlert("No vehicle entered to exit.");
                return;
            }
            IsStaffBarcode = true;
        }

        public void ExecuteBackCommand()
        {
            IsLoading = false;
            IsStaffBarcode = false;
        }

        public async void ExecuteLoad()
        {
            try
            {
                if (string.IsNullOrEmpty(Barcode))
                {
                    DependencyService.Get<IMessage>().ShortAlert("Enter Correct barcode ");                   
                    return;
                }
                IsLoading = true;
                var res = new FunctionResponse();
                if (Barcode.ToString().StartsWith("#"))
                {
                    if(POUT.PID == 0)
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Voucher cannot be accepted. No Vehicle barcode scanned.");
                        IsLoading = false;
                        return;
                    }
                    if (Vouchers.Any(x => x.Barcode.ToUpper() == Barcode.ToUpper().ToString()))
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Voucher already Accepted.");
                        IsLoading = false;
                        return;
                    }
                    var transferData = new BarCodeTransfer() { barcode = Barcode.ToUpper(), Vouchers = Vouchers, mDiscount = mDiscount, PIN = PIN, POUT = POUT };
                    res = await ParkingDetailsConnection.UploadVoucherCode(transferData);

                    if (res.status == "ok")
                        DependencyService.Get<IMessage>().ShortAlert("Voucher Discount applied Successfully");

                }
                else if (Barcode.ToUpper().ToString().StartsWith(GlobalClass.MemberBarcodePrefix))
                {
                    if (POUT.PID == 0)
                    {
                        DependencyService.Get<IMessage>().LongAlert("MemberCard cannot be accepted. No Vehicle barcode scanned.");
                        IsLoading = false;
                        return;
                    }
                    if (mDiscount != null && !string.IsNullOrEmpty(mDiscount.MemberId))
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Member already Accepted.");
                        IsLoading = false;
                        return;
                    }
                    var transferData = new BarCodeTransfer() { barcode = Barcode.ToUpper(), Vouchers = Vouchers, mDiscount = mDiscount, PIN = PIN, POUT = POUT };
                    res = await ParkingDetailsConnection.UploadMemberCode(transferData);
                    if (res.status == "ok")
                        DependencyService.Get<IMessage>().ShortAlert("Member Discount applied successfully");
                }
                else
                {
                    var transferData = new BarCodeTransfer() { barcode = Barcode.ToUpper(), PIN = PIN, POUT = POUT };
                   
                    res = await ParkingDetailsConnection.LoadParkingOutAsync(transferData);
                    if(res.status == "error")
                    {
                        PIN = new ParkingIn();
                        POUT = new ParkingOut();
                        Vouchers = new List<Voucher>();
                        mDiscount = new MemberDiscount();
                    }
                    if(res.status == "ok")
                        DependencyService.Get<IMessage>().ShortAlert("Details Loaded Successfully");
                }
                if (res.status == "ok")
                {
                    ParkingDetails = JsonConvert.DeserializeObject<BarCodeTransfer>(res.result.ToString());
                    if(ParkingDetails != null)
                    {
                        PIN = ParkingDetails.PIN;                       
                        POUT = ParkingDetails.POUT;
                        POUT.UID = GlobalClass.User.UID;
                        POUT.SESSION_ID = GlobalClass.User.Session;
                        Vouchers = ParkingDetails.Vouchers;
                        mDiscount = ParkingDetails.mDiscount;
                        Barcode = "";
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(res.Message);
                }
                IsLoading = false;
            }catch(Exception ex)
            {
                IsLoading = false;
                DependencyService.Get<IMessage>().ShortAlert(ex.Message);
            }
        }

        public async void ExecuteFinishCommand()
        {
            try
            {
                if (PIN == null || POUT == null)
                {
                    DependencyService.Get<IMessage>().ShortAlert("No Vehicle to exit");
                    return;
                }
                if (POUT.PID == 0 || PIN.PID == 0)
                {
                    DependencyService.Get<IMessage>().ShortAlert("No vehicle entered to exit.");
                    return;
                }
                var ans = await App.Current.MainPage.DisplayAlert("Confirm","Are you sure to Exit Vehicle?","Yes","No");
                if (!ans)
                    return;                

                IsLoading = true;
                var transferData = new BarCodeTransfer() { barcode = Barcode, Vouchers = Vouchers, mDiscount = mDiscount, PIN = PIN, POUT = POUT , Session = GlobalClass.User.Session};
                var res = await ParkingDetailsConnection.SavePOUT(transferData);
                if(res.status == "ok")
                {
                    DependencyService.Get<IMessage>().ShortAlert("Vehicle exited Successfully");
                    POUT = new ParkingOut();
                    PIN = new ParkingIn();
                    Vouchers = new List<Voucher>();
                    mDiscount = new MemberDiscount();
                }
                else if(res.status == "error")
                {
                    DependencyService.Get<IMessage>().ShortAlert(res.Message);
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Error: Server Connection Problem");
                }
                IsLoading = false;
            }
            catch(Exception ex)
            {
                IsLoading = false;
                DependencyService.Get<IMessage>().ShortAlert(ex.Message);
            }
        }

        public async void ExecuteStampCommand()
        {
            try
            {
                if (PIN == null || POUT == null)
                {
                    DependencyService.Get<IMessage>().ShortAlert("No Vehicle to exit");
                    return;
                }
                if (POUT.PID == 0 || PIN.PID == 0)
                {
                    DependencyService.Get<IMessage>().ShortAlert("No vehicle entered to exit.");
                    return;
                }

                var ans = await App.Current.MainPage.DisplayAlert("Confirm", "Are you sure to Exit Vehicle?", "Yes", "No");
                if (!ans)
                    return;

                POUT.SESSION_ID = GlobalClass.User.Session;
                POUT.STAFF_BARCODE = "STAMP";
                var transferData = new BarCodeTransfer() { barcode = Barcode, Vouchers = Vouchers, mDiscount = mDiscount, PIN = PIN, POUT = POUT, Session = GlobalClass.User.Session };
                var res = await ParkingDetailsConnection.SaveStaffOrStampPOUT(transferData);
                if (res.status == "ok")
                {
                    DependencyService.Get<IMessage>().ShortAlert("Vehicle exited Successfully");
                    POUT = new ParkingOut();
                    PIN = new ParkingIn();
                    Vouchers = new List<Voucher>();
                    mDiscount = new MemberDiscount();
                }
                else if(res.status == "error")
                {
                    DependencyService.Get<IMessage>().ShortAlert(res.Message);
                }

            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert(ex.Message);
            }
        }

        public async void ExecuteStaffCommand()
        {
            try
            {
                //IsStaffBarcode = false;
                if (string.IsNullOrEmpty(StaffBarcode))
                {
                    DependencyService.Get<IMessage>().ShortAlert("Invalid Barcode. Please Try Again");
                    return;
                }
               
                POUT.SESSION_ID = GlobalClass.User.Session;
                POUT.STAFF_BARCODE = StaffBarcode;
                var transferData = new BarCodeTransfer() { barcode = Barcode, Vouchers = Vouchers, mDiscount = mDiscount, PIN = PIN, POUT = POUT, Session = GlobalClass.User.Session };
                var res = await ParkingDetailsConnection.SaveStaffOrStampPOUT(transferData);
                if (res.status == "ok")
                {
                    IsStaffBarcode = false;
                    DependencyService.Get<IMessage>().ShortAlert("Vehicle exited Successfully");
                    POUT = new ParkingOut();
                    PIN = new ParkingIn();
                    Vouchers = new List<Voucher>();
                    mDiscount = new MemberDiscount();
                }
                else if (res.status == "error")
                {
                    DependencyService.Get<IMessage>().ShortAlert(res.Message);
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert(ex.Message);
            }
        }
    }
}
