using ParkingApp.Interfaces;
using ParkingCommonLibrary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ParkingApp.SQLiteAccess
{
    public class UserAccess
    {
        public static bool LoadUserAndIP(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<User>();
                    var apiRow = conn.Table<User>().ToList();
                    if (apiRow.Count > 0)
                    {
                        ParkingCommonLibrary.Helpers.GlobalClass.User = apiRow.FirstOrDefault();
                        Helpers.Constants.User = apiRow.FirstOrDefault();
                        
                        Helpers.Constants.SetMainURL(ParkingCommonLibrary.Helpers.GlobalClass.User);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert(e.Message);
                return false;
            }
        }

        public static bool SetUserAndIP(string DatabaseLocation, User User)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<User>();

                    conn.DeleteAll<User>();
                    int rows = conn.Insert(User);
                    return true;
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert(e.Message);
                return false;
            }
        }

        public static bool DeleteUserAndIP(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<User>();

                    conn.DeleteAll<User>();
                    return true;
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert(e.Message);
                return false;
            }
        }
    }
}
