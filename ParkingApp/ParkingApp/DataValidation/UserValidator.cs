using ParkingCommonLibrary.Models;
using ParkingCommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.DataValidation
{
    public class UserValidator
    {
        public static FunctionResponse CheckUser(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    return new FunctionResponse() { status = "error", Message = "UserName is Empty" };
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    return new FunctionResponse() { status = "error", Message = "Password is Empty" };
                }
                else if (string.IsNullOrEmpty(user.ip1) || string.IsNullOrEmpty(user.ip2) || string.IsNullOrEmpty(user.ip3) || string.IsNullOrEmpty(user.ip4) || string.IsNullOrEmpty(user.Port))
                {
                    return new FunctionResponse() { status = "error", Message = "IP Address is inCorrect" };
                }
                else
                {
                    return new FunctionResponse() { status = "ok" };
                }
            }
            catch(Exception ex) { return new FunctionResponse() { status = "error", Message = ex.Message }; }
        }

    }
}
