using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalbooking.Entities
{
    public class UnionLeader : IBooking
    {
        private int unionLeaderID;
        private string address;
        private string fullname;

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

        public int UnionLeaderID
        {
            get { return unionLeaderID; }
            set { unionLeaderID = value; }
        }

        public override string ToString()
        {
            return Fullname;
        }
    }
}
