using CampCadetWebAPI.Models;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class MeetingMembersAttendingConfiguration : EntityTypeConfiguration<MeetingMembersAttendingModel>
    {
        public MeetingMembersAttendingConfiguration()
        {
            ToTable("BoardMeetingMembersAttending");

            HasKey(m => new { m.BoardMeetingID, m.BoardMemberID});

            // Composite index - Making member unique to a 
            /*Property(m => m.BoardMeeting)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_RTUId_SerialNumber", 1) { IsUnique = true }
                    )
                );

            // Second half of the composite index
            Property(b => b.SerialNumber)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_RTUId_SerialNumber", 2) { IsUnique = true }
                    )
                );*/
        }
    }
}