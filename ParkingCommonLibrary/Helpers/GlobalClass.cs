using ParkingCommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ParkingCommonLibrary.Helpers
{
    public class GlobalClass
    {
        public static string DataConnectionString;
        //public static string Terminal;
        public static int GraceTime;
        public static string CompanyName;
        public static string CompanyAddress;
        public static string CompanyPan;
        public static decimal VAT = 13;
        //public static User User;
        public static string PrinterName = "POS80";
        public static DateTime BeginTime { get { return new DateTime(1900, 1, 1, 0, 0, 0, 0); } }
        public static DateTime EndTime { get { return new DateTime(1900, 1, 1, 23, 59, 59); } }
   
        public static int Session;
        public static bool EnablePlateNo { get; set; }
        public static byte AllowMultiVehicleForStaff;
        public static short DefaultMinVacantLot;
        public static string MemberBarcodePrefix;
        public static byte FYID = 3;
        public static string FYNAME;
        public static byte SettlementMode;
        public static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\IMS\\Parking";
        public static decimal AbbTaxInvoiceLimit = 5000;
        internal static bool NoRawPrinter;

        public static string ReportName { get; set; }
        public static string ReportParams { get; set; }
        public static string PrintTime { get; set; }
        public static byte SlipPrinterWith { get; set; }
        
        public static string Division = "MMX";
        public static string Terminal = "AAA";

        public static Exception LastException;
        public static string GetTime = "(SELECT CONVERT(VARCHAR,(SELECT GETDate()),8))";
        public static string GetDate = "(SELECT GETDATE())";                    


        public static string GetEncryptedPWD(string pwd, ref string Salt)
        {

            StringBuilder sBuilder;

            if (string.IsNullOrEmpty(Salt))
            {
                System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
                byte[] saltByte = new byte[8];
                rng.GetNonZeroBytes(saltByte);

                sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < saltByte.Length; i++)
                {
                    sBuilder.Append(saltByte[i].ToString("x2"));
                }

                Salt = sBuilder.ToString();
            }

            System.Security.Cryptography.SHA256CryptoServiceProvider sha = new System.Security.Cryptography.SHA256CryptoServiceProvider();
            //System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(pwd + Salt);
            data = sha.ComputeHash(data);

            sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

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

        public GlobalClass()
        {
        }

        //public static DateTime GetAdDate(string BS)
        //{
        //    try
        //    {
        //        DateTime AdDate;
        //        if (CnnMain.State == ConnectionState.Closed) CnnMain.Open();
        //        SqlCommand Cmd = new SqlCommand("Select AD from DATEMITI where MITI='" + BS + "'", CnnMain);
        //        using (SqlDataReader dr = Cmd.ExecuteReader())
        //        {
        //            if (dr.Read())
        //            {
        //                AdDate = Convert.ToDateTime(dr["AD"]);
        //                return AdDate;
        //            }
        //            else
        //            {
        //                throw new Exception(string.Format("Miti ({0}) is out of range.", BS));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        CnnMain.Close();
        //    }
        //}

        //public static string GetBSDate(DateTime Adate)
        //{
        //    try
        //    {
        //        using (SqlConnection Con = new SqlConnection(Helpers.ConnectionDbInfo.ConnectionString))
        //        {
        //            return Con.ExecuteScalar<string>("select dbo.dateToMiti('" + Adate.ToString("dd/MMM/yyyy") + "','/')");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //IMSErrorHandler.OnErrorCaught(new ImsExceptionArgs("Globalclass.GetBSDate", ex.GetBaseException().Message, ex));
        //        return string.Empty;
        //    }
        //}
    }
}
