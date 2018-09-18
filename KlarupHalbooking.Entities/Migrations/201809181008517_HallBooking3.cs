namespace KlarupHalbooking.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HallBooking3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HallBookings", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.HallBookings", "Admin_AdminID", c => c.Int());
            CreateIndex("dbo.HallBookings", "Admin_AdminID");
            AddForeignKey("dbo.HallBookings", "Admin_AdminID", "dbo.Admins", "AdminID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HallBookings", "Admin_AdminID", "dbo.Admins");
            DropIndex("dbo.HallBookings", new[] { "Admin_AdminID" });
            DropColumn("dbo.HallBookings", "Admin_AdminID");
            DropColumn("dbo.HallBookings", "Confirmed");
        }
    }
}
