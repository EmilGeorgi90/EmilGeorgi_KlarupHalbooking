using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalBooking.Entities
{
    public class Union : IBooking
    {
        private int unionID;
        private string unionName;
        private UserData userData;
        private UnionLeader unionLeader;

        public UnionLeader UnionLeader
        {
            get { return unionLeader; }
            set { unionLeader = value; }
        }

        public UserData UserData
        {
            get { return userData; }
            set { userData = value; }
        }

        public string UnionName
        {
            get { return unionName; }
            set { unionName = value; }
        }

        public int UnionID
        {
            get { return unionID; }
            set { unionID = value; }
        }
        private DateTime reservations;

        public DateTime Reservations
        {
            get { return reservations; }
            set { reservations = value; }
        }

        public override string ToString()
        {
            return UnionName;
        }
    }
}
