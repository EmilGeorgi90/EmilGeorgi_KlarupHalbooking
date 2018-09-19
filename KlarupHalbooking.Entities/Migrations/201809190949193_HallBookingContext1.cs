namespace KlarupHalBooking.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HallBookingContext1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Unions", "Reservations", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Unions", "Reservations");
        }
    }
}
