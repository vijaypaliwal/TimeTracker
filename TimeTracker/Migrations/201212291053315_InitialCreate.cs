namespace TimeTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ShortName = c.String(nullable: false),
                        iscool = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        MiddleName = c.String(),
                        IsLocked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trackers",
                c => new
                    {
                        TrackerID = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        TrakingDateTime = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrackerID)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.PatientID)
                .Index(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Trackers", new[] { "StatusId" });
            DropIndex("dbo.Trackers", new[] { "PatientID" });
            DropForeignKey("dbo.Trackers", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Trackers", "PatientID", "dbo.Patients");
            DropTable("dbo.Trackers");
            DropTable("dbo.Patients");
            DropTable("dbo.Status");
        }
    }
}
