namespace KlarupHalbooking.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HallBooking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        Fullname = c.String(),
                        Address = c.Int(nullable: false),
                        UserData_UserDataID = c.Int(),
                    })
                .PrimaryKey(t => t.AdminID)
                .ForeignKey("dbo.UserDatas", t => t.UserData_UserDataID)
                .Index(t => t.UserData_UserDataID);
            
            CreateTable(
                "dbo.UserDatas",
                c => new
                    {
                        UserDataID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Phonenumber = c.String(),
                    })
                .PrimaryKey(t => t.UserDataID);
            
            CreateTable(
                "dbo.HallBookings",
                c => new
                    {
                        HallBookingID = c.Int(nullable: false, identity: true),
                        HallBookingTime = c.DateTime(nullable: false),
                        Activity_ActivityID = c.Int(),
                        Union_UnionID = c.Int(),
                    })
                .PrimaryKey(t => t.HallBookingID)
                .ForeignKey("dbo.Activities", t => t.Activity_ActivityID)
                .ForeignKey("dbo.Unions", t => t.Union_UnionID)
                .Index(t => t.Activity_ActivityID)
                .Index(t => t.Union_UnionID);
            
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityID = c.Int(nullable: false, identity: true),
                        ActivityName = c.String(),
                    })
                .PrimaryKey(t => t.ActivityID);
            
            CreateTable(
                "dbo.Unions",
                c => new
                    {
                        UnionID = c.Int(nullable: false, identity: true),
                        UnionName = c.String(),
                        UnionLeader_UnionLeaderID = c.Int(),
                        UserData_UserDataID = c.Int(),
                    })
                .PrimaryKey(t => t.UnionID)
                .ForeignKey("dbo.UnionLeaders", t => t.UnionLeader_UnionLeaderID)
                .ForeignKey("dbo.UserDatas", t => t.UserData_UserDataID)
                .Index(t => t.UnionLeader_UnionLeaderID)
                .Index(t => t.UserData_UserDataID);
            
            CreateTable(
                "dbo.UnionLeaders",
                c => new
                    {
                        UnionLeaderID = c.Int(nullable: false, identity: true),
                        Fullname = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.UnionLeaderID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HallBookings", "Union_UnionID", "dbo.Unions");
            DropForeignKey("dbo.Unions", "UserData_UserDataID", "dbo.UserDatas");
            DropForeignKey("dbo.Unions", "UnionLeader_UnionLeaderID", "dbo.UnionLeaders");
            DropForeignKey("dbo.HallBookings", "Activity_ActivityID", "dbo.Activities");
            DropForeignKey("dbo.Admins", "UserData_UserDataID", "dbo.UserDatas");
            DropIndex("dbo.Unions", new[] { "UserData_UserDataID" });
            DropIndex("dbo.Unions", new[] { "UnionLeader_UnionLeaderID" });
            DropIndex("dbo.HallBookings", new[] { "Union_UnionID" });
            DropIndex("dbo.HallBookings", new[] { "Activity_ActivityID" });
            DropIndex("dbo.Admins", new[] { "UserData_UserDataID" });
            DropTable("dbo.UnionLeaders");
            DropTable("dbo.Unions");
            DropTable("dbo.Activities");
            DropTable("dbo.HallBookings");
            DropTable("dbo.UserDatas");
            DropTable("dbo.Admins");
        }
    }
}
