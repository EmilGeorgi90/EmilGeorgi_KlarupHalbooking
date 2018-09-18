using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalbooking.Entities
{
    public class UserData : IBooking
    {
        private int userDataID;
        private string username;
        private string password;
        private string phonenumber;

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

        public override string ToString()
        {
            return Username;
        }
    }
}
