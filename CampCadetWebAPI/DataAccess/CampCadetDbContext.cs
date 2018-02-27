using CampCadetWebAPI.DataAccess.Entity_Configurations;
using CampCadetWebAPI.Models;
using System.Configuration;
using System.Data.Entity;

namespace CampCadetWebAPI.DataAccess
{
    public class CampCadetDbContext : DbContext
    {
        public CampCadetDbContext() : base("name=CampCadet") { }

        #region DbSets
        public virtual DbSet<BlurbModel> Blurbs { get; set; }
        public virtual DbSet<MeetingLocationModel> MeetingLocations { get; set; }
        public virtual DbSet<MeetingMembersAttendingModel> MeetingMembersAttendings { get; set; }
        public virtual DbSet<MeetingNoteModel> MeetingNotes { get; set; }
        public virtual DbSet<MeetingModel> Meetings { get; set; }
        public virtual DbSet<MemberCategoryModel> MemberCategories { get; set; }
        public virtual DbSet<MemberModel> Members { get; set; }
        public virtual DbSet<CampDateModel> CampDates { get; set; }
        public virtual DbSet<CampRuleModel> CampRules { get; set; }
        public virtual DbSet<CampSupplyModel> CampSupplies { get; set; }
        public virtual DbSet<ContactCategoryModel> ContactCategories { get; set; }
        public virtual DbSet<ContactReasonCategoryModel> ContactReasonCategories { get; set; }
        public virtual DbSet<DonorCategoryModel> DonorCategories { get; set; }
        public virtual DbSet<DonorCategoryLinkModel> DonorCategoryLinks { get; set; }
        public virtual DbSet<DonorLevelModel> DonorLevels { get; set; }
        public virtual DbSet<DonorModel> Donors { get; set; }
        public virtual DbSet<DownloadModel> Downloads { get; set; }
        public virtual DbSet<EligibilityRequirementModel> EligibilityRequirements { get; set; }
        public virtual DbSet<FAQCategoryModel> FAQCategories { get; set; }
        public virtual DbSet<FAQModel> FAQs { get; set; }
        public virtual DbSet<FundraisingEventModel> FundraisingEvents { get; set; }
        public virtual DbSet<LinkCategoryModel> LinkCategories { get; set; }
        public virtual DbSet<LinkModel> Links { get; set; }
        public virtual DbSet<MiscConfigurationModel> MiscConfigurations { get; set; }
        public virtual DbSet<ScheduleModel> Schedules { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var schemaName = ConfigurationManager.AppSettings["DbSchemaName"];

            // This is here to prevent the app from throwing exceptions if the DB structure changes
            Database.SetInitializer<CampCadetDbContext>(null);

            modelBuilder.HasDefaultSchema(schemaName);

            #region Configurations
            modelBuilder.Configurations.Add(new BlurbConfiguration());
            modelBuilder.Configurations.Add(new MeetingConfiguration());
            modelBuilder.Configurations.Add(new MeetingLocationConfiguration());
            modelBuilder.Configurations.Add(new MeetingMembersAttendingConfiguration());
            modelBuilder.Configurations.Add(new MeetingNoteConfiguration());
            modelBuilder.Configurations.Add(new MemberCategoryConfiguration());
            modelBuilder.Configurations.Add(new MemberConfiguration());
            modelBuilder.Configurations.Add(new CampDateConfiguration());
            modelBuilder.Configurations.Add(new CampRuleConfiguration());
            modelBuilder.Configurations.Add(new CampSupplyConfiguration());
            modelBuilder.Configurations.Add(new ContactCategoryConfiguration());
            modelBuilder.Configurations.Add(new ContactReasonCategoryConfiguration());
            modelBuilder.Configurations.Add(new DonorCategoryConfiguration());
            modelBuilder.Configurations.Add(new DonorCategoryLinkConfiguration());
            modelBuilder.Configurations.Add(new DonorConfiguration());
            modelBuilder.Configurations.Add(new DonorLevelConfiguration());
            modelBuilder.Configurations.Add(new DownloadConfiguration());
            modelBuilder.Configurations.Add(new EligibilityRequirementConfiguration());
            modelBuilder.Configurations.Add(new FAQCategoryConfiguration());
            modelBuilder.Configurations.Add(new FAQConfiguration());
            modelBuilder.Configurations.Add(new FundraisingEventConfiguration());
            modelBuilder.Configurations.Add(new LinkCategoryConfiguration());
            modelBuilder.Configurations.Add(new LinkConfiguration());
            modelBuilder.Configurations.Add(new MiscConfigurationConfiguration());
            modelBuilder.Configurations.Add(new ScheduleConfiguration());
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        void tmp()
        {
            /*modelBuilder.Entity<BoardCategory>()
                .HasMany(e => e.BoardMembers)
                .WithRequired(e => e.BoardCategory)
                .HasForeignKey(e => e.CategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BoardMeetingLocation>()
                .HasMany(e => e.BoardMeetings)
                .WithRequired(e => e.BoardMeetingLocation)
                .HasForeignKey(e => e.LocationID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BoardMeetingNote>()
                .Property(e => e.Topic)
                .IsUnicode(false);

            modelBuilder.Entity<BoardMeetingNote>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<BoardMeeting>()
                .HasMany(e => e.BoardMeetingMembersAttendings)
                .WithRequired(e => e.BoardMeeting)
                .HasForeignKey(e => e.MeetingID);

            modelBuilder.Entity<BoardMeeting>()
                .HasMany(e => e.BoardMeetingNotes)
                .WithRequired(e => e.BoardMeeting)
                .HasForeignKey(e => e.MeetingID);

            modelBuilder.Entity<BoardMember>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<BoardMember>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<BoardMember>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<BoardMember>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<BoardMember>()
                .Property(e => e.Prefix)
                .IsUnicode(false);

            modelBuilder.Entity<BoardMember>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<BoardMember>()
                .HasMany(e => e.BoardMeetingMembersAttendings)
                .WithRequired(e => e.BoardMember)
                .HasForeignKey(e => e.MemberID);

            modelBuilder.Entity<CampRule>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<CampSupply>()
                .Property(e => e.Item)
                .IsUnicode(false);

            modelBuilder.Entity<ContactCategory>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ContactReasonCategory>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<DonorCategory>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<DonorCategoryLink>()
                .Property(e => e.AmountGiven)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DonorCategoryLink>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<DonorLevel>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<DonorLevel>()
                .Property(e => e.AmountLower)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DonorLevel>()
                .Property(e => e.Color)
                .IsUnicode(false);

            modelBuilder.Entity<DonorLevel>()
                .Property(e => e.AmountUpper)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DonorLevel>()
                .Property(e => e.FontColor)
                .IsUnicode(false);

            modelBuilder.Entity<Donor>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Donor>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<Donor>()
                .Property(e => e.Website)
                .IsUnicode(false);

            modelBuilder.Entity<Donor>()
                .Property(e => e.IconURL)
                .IsUnicode(false);

            modelBuilder.Entity<Download>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<Download>()
                .Property(e => e.UploadedBy)
                .IsUnicode(false);

            modelBuilder.Entity<EligibilityRequirement>()
                .Property(e => e.Requirement)
                .IsUnicode(false);

            modelBuilder.Entity<FAQCategory>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<FAQCategory>()
                .HasMany(e => e.FAQs)
                .WithRequired(e => e.FAQCategory)
                .HasForeignKey(e => e.CategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FAQ>()
                .Property(e => e.Question)
                .IsUnicode(false);

            modelBuilder.Entity<FAQ>()
                .Property(e => e.Answer)
                .IsUnicode(false);

            modelBuilder.Entity<FundraisingEvent>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<FundraisingEvent>()
                .Property(e => e.Details)
                .IsUnicode(false);

            modelBuilder.Entity<LinkCategory>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<LinkCategory>()
                .HasMany(e => e.Links)
                .WithRequired(e => e.LinkCategory)
                .HasForeignKey(e => e.CategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Link>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Link>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<MiscConfiguration>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<MiscConfiguration>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<Schedule>()
                .Property(e => e.Event)
                .IsUnicode(false);

            modelBuilder.Entity<Schedule>()
                .Property(e => e.InfoURL)
                .IsUnicode(false);

            modelBuilder.Entity<Schedule>()
                .Property(e => e.AdditionalInfo)
                .IsUnicode(false);*/
        }
    }
}