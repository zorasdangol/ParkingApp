using Newtonsoft.Json;
using ParkingCommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApp.DataAccess
{
    public class LoginConnection
    {
        public static string GenerateLoginURL()
        {
            return string.Format(Helpers.Constants.MainURL + Helpers.Constants.LoginURL);
        }
        
        public static async Task<FunctionResponse> UserVerficationAsync(User User)
        {
            try
            {
                FunctionResponse functionResponse = new FunctionResponse();
                
                var JsonObject = JsonConvert.SerializeObject(User);
                string ContentType = "application/json"; // or application/xml

                String url = GenerateLoginURL();

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(url, new StringContent(JsonObject.ToString(), Encoding.UTF8, ContentType));

                    var json = await response.Content.ReadAsStringAsync();

                    functionResponse = JsonConvert.DeserializeObject<FunctionResponse>(json);
                    
                    return functionResponse;
                }
            }
            catch (Exception e)
            {
                return new FunctionResponse() { status = "error", Message = "Cannot connect to Server" };
            }
        }
                
    }
}
