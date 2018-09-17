using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalbooking.Entities
{
    public class UserData
    {
        private int userDataID;
        private string username;
        private string password;
        private string phonenumber;

        public UserData(string username, string password, string phonenumber)
        {
            Username = username;
            Password = password;
            Phonenumber = phonenumber;
        }

        public UserData(int userDataID, string username, string password, string phonenumber)
        {
            UserDataID = userDataID;
            Username = username;
            Password = password;
            Phonenumber = phonenumber;
        }
        public int UserDataID
        {
            get { return userDataID; }
            set { userDataID = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        

        public string Phonenumber
        {
            get { return phonenumber; }
            set { phonenumber = value; }
        }
    }
}
