namespace CampCadetWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("CampCadet.CampDates", "EndDate", c => c.DateTime());
            AlterColumn("CampCadet.CampRules", "Description", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("CampCadet.CampSupplies", "Item", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("CampCadet.DonorCategories", "Enabled", c => c.Boolean());
            AlterColumn("CampCadet.DonorCategories", "SortPriority", c => c.Int());
            AlterColumn("CampCadet.EligibilityRequirements", "Requirement", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("CampCadet.FAQs", "Question", c => c.String(nullable: false, maxLength: 400));
            CreateIndex("CampCadet.Blurbs", "Name", unique: true, name: "IX_BlurbName");
            CreateIndex("CampCadet.CampRules", "Description", unique: true, name: "IX_RuleDescription");
            CreateIndex("CampCadet.CampSupplies", "Item", unique: true, name: "IX_SupplyItem");
            CreateIndex("CampCadet.ContactCategories", "Description", unique: true, name: "IX_ContactCategoryDescription");
            CreateIndex("CampCadet.ContactReasonCategories", "Description", unique: true, name: "IX_ContactReasonCategoryDescription");
            CreateIndex("CampCadet.DonorCategories", "Description", unique: true, name: "IX_DonorCategoryDescription");
            CreateIndex("CampCadet.Donors", "Name", unique: true, name: "IX_DonorName");
            CreateIndex("CampCadet.DonorLevels", "Description", unique: true, name: "IX_DonorLevelDescription");
            CreateIndex("CampCadet.Downloads", "FileName", unique: true, name: "IX_DownloadFileName");
            CreateIndex("CampCadet.EligibilityRequirements", "Requirement", unique: true, name: "IX_EligibilityRequirement");
            CreateIndex("CampCadet.FAQCategories", "Description", unique: true, name: "IX_FAQCategoryDescription");
            CreateIndex("CampCadet.FAQs", "Question", unique: true, name: "IX_FAQQuestion");
            CreateIndex("CampCadet.FundraisingEvents", "Description", unique: true, name: "IX_EventDescription");
            CreateIndex("CampCadet.LinkCategories", "Description", unique: true, name: "IX_LinkCategoryDescription");
            CreateIndex("CampCadet.Links", "Description", unique: true, name: "IX_LinkDescription");
            CreateIndex("CampCadet.BoardMeetingLocations", "Description", unique: true, name: "IX_MeetingLocationDescription");
            CreateIndex("CampCadet.BoardMeetings", "DateTime", unique: true, name: "IX_MeetingDate");
            CreateIndex("CampCadet.BoardMembers", new[] { "FirstName", "LastName" }, unique: true, name: "IX_MemberName");
            CreateIndex("CampCadet.BoardMemberCategories", "Description", unique: true, name: "IX_MemberCategoryDescription");
            CreateIndex("CampCadet.MiscConfiguration", "Description", unique: true, name: "IX_ConfigurationDescription");
        }
        
        public override void Down()
        {
            DropIndex("CampCadet.MiscConfiguration", "IX_ConfigurationDescription");
            DropIndex("CampCadet.BoardMemberCategories", "IX_MemberCategoryDescription");
            DropIndex("CampCadet.BoardMembers", "IX_MemberName");
            DropIndex("CampCadet.BoardMeetings", "IX_MeetingDate");
            DropIndex("CampCadet.BoardMeetingLocations", "IX_MeetingLocationDescription");
            DropIndex("CampCadet.Links", "IX_LinkDescription");
            DropIndex("CampCadet.LinkCategories", "IX_LinkCategoryDescription");
            DropIndex("CampCadet.FundraisingEvents", "IX_EventDescription");
            DropIndex("CampCadet.FAQs", "IX_FAQQuestion");
            DropIndex("CampCadet.FAQCategories", "IX_FAQCategoryDescription");
            DropIndex("CampCadet.EligibilityRequirements", "IX_EligibilityRequirement");
            DropIndex("CampCadet.Downloads", "IX_DownloadFileName");
            DropIndex("CampCadet.DonorLevels", "IX_DonorLevelDescription");
            DropIndex("CampCadet.Donors", "IX_DonorName");
            DropIndex("CampCadet.DonorCategories", "IX_DonorCategoryDescription");
            DropIndex("CampCadet.ContactReasonCategories", "IX_ContactReasonCategoryDescription");
            DropIndex("CampCadet.ContactCategories", "IX_ContactCategoryDescription");
            DropIndex("CampCadet.CampSupplies", "IX_SupplyItem");
            DropIndex("CampCadet.CampRules", "IX_RuleDescription");
            DropIndex("CampCadet.Blurbs", "IX_BlurbName");
            AlterColumn("CampCadet.FAQs", "Question", c => c.String(nullable: false, maxLength: 900));
            AlterColumn("CampCadet.EligibilityRequirements", "Requirement", c => c.String(nullable: false, maxLength: 900));
            AlterColumn("CampCadet.DonorCategories", "SortPriority", c => c.Int(nullable: false));
            AlterColumn("CampCadet.DonorCategories", "Enabled", c => c.Boolean(nullable: false));
            AlterColumn("CampCadet.CampSupplies", "Item", c => c.String(nullable: false, maxLength: 900));
            AlterColumn("CampCadet.CampRules", "Description", c => c.String(nullable: false, maxLength: 900));
            AlterColumn("CampCadet.CampDates", "EndDate", c => c.DateTime(nullable: false));
        }
    }
}
