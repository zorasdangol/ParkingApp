using Newtonsoft.Json;
using ParkingCommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApp.DataAccess
{
    public class ParkingDetailsConnection
    {
        public static string GenerateParkingOutDetailsURL()
        {
            return string.Format(Helpers.Constants.MainURL + Helpers.Constants.ParkingOutDetailsURL);
        }

        public static string GenerateCheckVoucherCodeURL()
        {
            return string.Format(Helpers.Constants.MainURL + Helpers.Constants.CheckVoucherCodeURL);
        }

        public static string GenerateCheckMemberCodeURL()
        {
            return string.Format(Helpers.Constants.MainURL + Helpers.Constants.CheckMemberCodeURL);
        }

        public static string GenerateSavePOUTURL()
        {
            return string.Format(Helpers.Constants.MainURL + Helpers.Constants.SavePOUTURL);
        }

        public static string GenerateSaveStaffOrStampPOUTURL()
        {
            return string.Format(Helpers.Constants.MainURL + Helpers.Constants.SaveStaffOrStampPOUTURL);
        }

        public static async Task<FunctionResponse> LoadParkingOutAsync(BarCodeTransfer transferData)
        {
            try
            {
                FunctionResponse functionResponse = new FunctionResponse();

                var JsonObject = JsonConvert.SerializeObject(transferData);
                string ContentType = "application/json"; // or application/xml

                String url = GenerateParkingOutDetailsURL();

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(url, new StringContent(JsonObject.ToString(), Encoding.UTF8, ContentType));
                    
                    var json = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<FunctionResponse>(json);
                    
                    return result;
                }
            }
            catch (Exception e)
            {
                return new FunctionResponse() { status = "error", Message = "Cannot connect to Server" };
            }
        }

        public static async Task<FunctionResponse> UploadVoucherCode(BarCodeTransfer barCodeTransfer)
        {
            try
            {
                FunctionResponse functionResponse = new FunctionResponse();

                var JsonObject = JsonConvert.SerializeObject(barCodeTransfer);
                string ContentType = "application/json"; // or application/xml

                String url = GenerateCheckVoucherCodeURL();

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(url, new StringContent(JsonObject.ToString(), Encoding.UTF8, ContentType));
                                        
                    var json = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<FunctionResponse>(json);

                    return result;
                }
            }
            catch (Exception e)
            {
                return new FunctionResponse() { status = "error", Message = "Cannot connect to Server" };
            }
        }

        public static async Task<FunctionResponse> UploadMemberCode(BarCodeTransfer barCodeTransfer)
        {
            try
            {
                FunctionResponse functionResponse = new FunctionResponse();

                var JsonObject = JsonConvert.SerializeObject(barCodeTransfer);
                string ContentType = "application/json"; // or application/xml

                String url = GenerateCheckMemberCodeURL();

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(url, new StringContent(JsonObject.ToString(), Encoding.UTF8, ContentType));

                    var json = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<FunctionResponse>(json);

                    return result;
                }
            }
            catch (Exception e)
            {
                return new FunctionResponse() { status = "error", Message = "Cannot connect to Server" };
            }
        }

        public static async Task<FunctionResponse> SavePOUT(BarCodeTransfer barCodeTransfer)
        {
            try
            {
                FunctionResponse functionResponse = new FunctionResponse();

                var JsonObject = JsonConvert.SerializeObject(barCodeTransfer);
                string ContentType = "application/json"; // or application/xml

                String url = GenerateSavePOUTURL();

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(url, new StringContent(JsonObject.ToString(), Encoding.UTF8, ContentType));

                    var json = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<FunctionResponse>(json);

                    return result;
                }
            }
            catch (Exception e)
            {
                return new FunctionResponse() { status = "error", Message = "Cannot connect to Server" };
            }
        }

        public static async Task<FunctionResponse> SaveStaffOrStampPOUT(BarCodeTransfer barCodeTransfer)
        {
            try
            {
                FunctionResponse functionResponse = new FunctionResponse();

                var JsonObject = JsonConvert.SerializeObject(barCodeTransfer);
                string ContentType = "application/json"; // or application/xml

                String url = GenerateSaveStaffOrStampPOUTURL();

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(url, new StringContent(JsonObject.ToString(), Encoding.UTF8, ContentType));

                    var json = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<FunctionResponse>(json);

                    return result;
                }
            }
            catch (Exception e)
            {
                return new FunctionResponse() { status = "error", Message = "Cannot connect to Server" };
            }
        }
    }
}
