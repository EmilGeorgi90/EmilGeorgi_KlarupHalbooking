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

        public UnionLeader()
        {
        }

        public UnionLeader(string fullname, string address)
        {
            Fullname = fullname;
            Address = address;
        }

        public UnionLeader(string fullname, string address, int unionLeaderID)
        {
            Fullname = fullname;
            Address = address;
            UnionLeaderID = unionLeaderID;
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

        public int UnionLeaderID
        {
            get { return unionLeaderID; }
            set { unionLeaderID = value; }
        }

    }
}
