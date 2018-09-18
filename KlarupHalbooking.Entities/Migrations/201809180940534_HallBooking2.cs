namespace KlarupHalbooking.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HallBooking2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HallBookings", "HallBookingEndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HallBookings", "HallBookingEndTime");
        }
    }
}
