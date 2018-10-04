using ParkingCommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Helpers
{
    public class Constants
    {
        public static User User = new User()
        {
            UserName = "admin",
            Password = "16877",
            ip1 = "192",
            ip2 = "168",
            ip3 = "125",
            ip4 = "173",
            Port = "80",
        };

        public static string IPAddress = User.ip1 + "." + User.ip2 + "." + User.ip3 + "." + User.ip4 + ":" + User.Port;

        public static string MainURL = "http://" + IPAddress + "/Parking/api/";
        
        public static void SetMainURL(User User)
        {
            IPAddress = User.ip1 + "." + User.ip2 + "." + User.ip3 + "." + User.ip4 + ":" + User.Port;

            MainURL = "http://" + IPAddress + "/Parking/api/";
        }

        public static string SetApiURL(string apiUrl)
        {
            return (MainURL + apiUrl);
        }

        public static string LoginURL = "userVerification";
        public static string ParkingOutDetailsURL = "CheckParkingSlip";
        public static string CheckVoucherCodeURL = "CheckVoucherCode";
        public static string CheckMemberCodeURL = "CheckMemberCode";
        public static string SavePOUTURL = "SavePOUT";

        public static string SaveStaffOrStampPOUTURL = "SaveStaffOrStampPOUT";

    }
}
