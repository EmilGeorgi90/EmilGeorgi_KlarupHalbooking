using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalBooking.Entities
{
    [Serializable]
    public enum ServerCommands
    {
        GetNextBooking,GetBookingFromUnionName,HallTimeTaken,UnionUsedHallMost
    }
}
