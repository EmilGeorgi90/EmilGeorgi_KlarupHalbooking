using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalBooking.Entities
{
    public class HallBookingContext : DbContext
    {
        public DbSet<UserData> UserData{ get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<UnionLeader> UnionLeaders { get; set; }
        public DbSet<Union> Unions { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<HallBooking> HallBookings { get; set; }
    }
}
