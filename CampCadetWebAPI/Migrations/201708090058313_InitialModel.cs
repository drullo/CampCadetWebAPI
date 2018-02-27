namespace CampCadetWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CampCadet.Blurbs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Blurb = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.BoardMeetingLocations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.BoardMeetings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Notes = c.String(),
                        BoardMeetingLocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CampCadet.BoardMeetingLocations", t => t.BoardMeetingLocationID, cascadeDelete: true)
                .Index(t => t.BoardMeetingLocationID);
            
            CreateTable(
                "CampCadet.BoardMeetingMembersAttending",
                c => new
                    {
                        BoardMeetingID = c.Int(nullable: false),
                        BoardMemberID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BoardMeetingID, t.BoardMemberID })
                .ForeignKey("CampCadet.BoardMembers", t => t.BoardMemberID, cascadeDelete: true)
                .ForeignKey("CampCadet.BoardMeetings", t => t.BoardMeetingID, cascadeDelete: true)
                .Index(t => t.BoardMeetingID)
                .Index(t => t.BoardMemberID);
            
            CreateTable(
                "CampCadet.BoardMembers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Title = c.String(maxLength: 250),
                        Description = c.String(maxLength: 500),
                        Prefix = c.String(maxLength: 50),
                        Email = c.String(maxLength: 100),
                        SortPriority = c.Int(),
                        Enabled = c.Boolean(),
                        ShowEmail = c.Boolean(),
                        BoardMemberCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CampCadet.BoardMemberCategories", t => t.BoardMemberCategoryID, cascadeDelete: true)
                .Index(t => t.BoardMemberCategoryID);
            
            CreateTable(
                "CampCadet.BoardMemberCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.BoardMeetingNotes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Topic = c.String(nullable: false, maxLength: 250),
                        Notes = c.String(),
                        PublicInfo = c.Boolean(nullable: false),
                        BoardMeetingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CampCadet.BoardMeetings", t => t.BoardMeetingID, cascadeDelete: true)
                .Index(t => t.BoardMeetingID);
            
            CreateTable(
                "CampCadet.CampDates",
                c => new
                    {
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ApplicationDeadline = c.DateTime(),
                        ApplicationsAvailableBeginning = c.DateTime(),
                        OrientationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.StartDate);
            
            CreateTable(
                "CampCadet.CampRules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 900),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.CampSupplies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Item = c.String(nullable: false, maxLength: 900),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.ContactCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.ContactReasonCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.DonorCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                        Enabled = c.Boolean(nullable: false),
                        SortPriority = c.Int(nullable: false),
                        ShowDonorLevels = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.DonorCategoryLinks",
                c => new
                    {
                        DonorID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        AmountGiven = c.Decimal(storeType: "money"),
                        Notes = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => new { t.DonorID, t.CategoryID })
                .ForeignKey("CampCadet.Donors", t => t.DonorID, cascadeDelete: true)
                .ForeignKey("CampCadet.DonorCategories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.DonorID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "CampCadet.Donors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        DisplayOnWebsite = c.Boolean(nullable: false),
                        Notes = c.String(),
                        SortPriority = c.Int(nullable: false),
                        Website = c.String(maxLength: 250),
                        IconURL = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.DonorLevels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                        AmountLower = c.Decimal(nullable: false, storeType: "money"),
                        Color = c.String(nullable: false, maxLength: 10),
                        AmountUpper = c.Decimal(nullable: false, storeType: "money"),
                        FontColor = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.Downloads",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 255),
                        AllowDownload = c.Boolean(nullable: false),
                        UploadedBy = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.EligibilityRequirements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Requirement = c.String(nullable: false, maxLength: 900),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.FAQCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.FAQs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Question = c.String(nullable: false, maxLength: 900),
                        Answer = c.String(nullable: false, maxLength: 1000),
                        FAQCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CampCadet.FAQCategories", t => t.FAQCategoryID, cascadeDelete: true)
                .Index(t => t.FAQCategoryID);
            
            CreateTable(
                "CampCadet.FundraisingEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 200),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.LinkCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.Links",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        URL = c.String(nullable: false, maxLength: 500),
                        LinkCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CampCadet.LinkCategories", t => t.LinkCategoryID, cascadeDelete: true)
                .Index(t => t.LinkCategoryID);
            
            CreateTable(
                "CampCadet.MiscConfiguration",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CampCadet.Schedule",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Event = c.String(nullable: false, maxLength: 500),
                        InfoURL = c.String(maxLength: 500),
                        AdditionalInfo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("CampCadet.Links", "LinkCategoryID", "CampCadet.LinkCategories");
            DropForeignKey("CampCadet.FAQs", "FAQCategoryID", "CampCadet.FAQCategories");
            DropForeignKey("CampCadet.DonorCategoryLinks", "CategoryID", "CampCadet.DonorCategories");
            DropForeignKey("CampCadet.DonorCategoryLinks", "DonorID", "CampCadet.Donors");
            DropForeignKey("CampCadet.BoardMeetings", "BoardMeetingLocationID", "CampCadet.BoardMeetingLocations");
            DropForeignKey("CampCadet.BoardMeetingNotes", "BoardMeetingID", "CampCadet.BoardMeetings");
            DropForeignKey("CampCadet.BoardMeetingMembersAttending", "BoardMeetingID", "CampCadet.BoardMeetings");
            DropForeignKey("CampCadet.BoardMembers", "BoardMemberCategoryID", "CampCadet.BoardMemberCategories");
            DropForeignKey("CampCadet.BoardMeetingMembersAttending", "BoardMemberID", "CampCadet.BoardMembers");
            DropIndex("CampCadet.Links", new[] { "LinkCategoryID" });
            DropIndex("CampCadet.FAQs", new[] { "FAQCategoryID" });
            DropIndex("CampCadet.DonorCategoryLinks", new[] { "CategoryID" });
            DropIndex("CampCadet.DonorCategoryLinks", new[] { "DonorID" });
            DropIndex("CampCadet.BoardMeetingNotes", new[] { "BoardMeetingID" });
            DropIndex("CampCadet.BoardMembers", new[] { "BoardMemberCategoryID" });
            DropIndex("CampCadet.BoardMeetingMembersAttending", new[] { "BoardMemberID" });
            DropIndex("CampCadet.BoardMeetingMembersAttending", new[] { "BoardMeetingID" });
            DropIndex("CampCadet.BoardMeetings", new[] { "BoardMeetingLocationID" });
            DropTable("CampCadet.Schedule");
            DropTable("CampCadet.MiscConfiguration");
            DropTable("CampCadet.Links");
            DropTable("CampCadet.LinkCategories");
            DropTable("CampCadet.FundraisingEvents");
            DropTable("CampCadet.FAQs");
            DropTable("CampCadet.FAQCategories");
            DropTable("CampCadet.EligibilityRequirements");
            DropTable("CampCadet.Downloads");
            DropTable("CampCadet.DonorLevels");
            DropTable("CampCadet.Donors");
            DropTable("CampCadet.DonorCategoryLinks");
            DropTable("CampCadet.DonorCategories");
            DropTable("CampCadet.ContactReasonCategories");
            DropTable("CampCadet.ContactCategories");
            DropTable("CampCadet.CampSupplies");
            DropTable("CampCadet.CampRules");
            DropTable("CampCadet.CampDates");
            DropTable("CampCadet.BoardMeetingNotes");
            DropTable("CampCadet.BoardMemberCategories");
            DropTable("CampCadet.BoardMembers");
            DropTable("CampCadet.BoardMeetingMembersAttending");
            DropTable("CampCadet.BoardMeetings");
            DropTable("CampCadet.BoardMeetingLocations");
            DropTable("CampCadet.Blurbs");
        }
    }
}
