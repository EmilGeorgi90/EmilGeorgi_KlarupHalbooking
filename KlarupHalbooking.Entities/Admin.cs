using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalbooking.Entities
{
    public class Admin
    {
        private int address;
        private string fullname;
        private UserData userData;
        private int adminID;

        public Admin(UserData userData, string fullname, int address)
        {
            UserData = userData;
            Fullname = fullname;
            Address = address;
        }

        public Admin(int adminID, UserData userData, string fullname, int address)
        {
            AdminID = adminID;
            UserData = userData;
            Fullname = fullname;
            Address = address;
        }

        public int AdminID
        {
            get { return adminID; }
            set { adminID = value; }
        }

        public UserData UserData
        {
            get { return userData; }
            set { userData = value; }
        }

        public string Fullname
        {
            get { return fullname; }
            set { fullname = value; }
        }

        public int Address
        {
            get { return address; }
            set { address = value; }
        }
    }
}
