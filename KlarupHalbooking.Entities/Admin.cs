using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalBooking.Entities
{
    public class Admin : IBooking
    {
        private string address;
        private string fullname;
        private UserData userData;
        private int adminID;

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

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public override string ToString()
        {
            return Fullname;
        }
    }
}
