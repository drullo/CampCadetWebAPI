using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class MeetingConfiguration : EntityTypeConfiguration<MeetingModel>
    {
        public MeetingConfiguration()
        {
            ToTable("BoardMeetings");

            Property(m => m.DateTime)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_MeetingDate") { IsUnique = true }
                    )
                );

            HasMany(m => m.BoardMeetingNotes)
                .WithRequired(n => n.BoardMeeting)
                .HasForeignKey(n => n.BoardMeetingID)
                .WillCascadeOnDelete(true);

            HasMany(m => m.BoardMeetingMembers)
                .WithRequired(m => m.BoardMeeting)
                .HasForeignKey(m => m.BoardMeetingID)
                .WillCascadeOnDelete(true);
        }
    }
}