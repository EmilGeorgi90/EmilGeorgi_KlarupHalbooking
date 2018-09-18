namespace KlarupHalbooking.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HallBooking1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "SpaceNeeded", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "SpaceNeeded");
        }
    }
}
