using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class MeetingLocationConfiguration : EntityTypeConfiguration<MeetingLocationModel>
    {
        public MeetingLocationConfiguration()
        {
            ToTable("BoardMeetingLocations");

            Property(l => l.Description)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_MeetingLocationDescription") { IsUnique = true }
                    )
                );

            HasMany(l => l.BoardMeetings)
                .WithRequired(m => m.BoardMeetingLocation)
                .HasForeignKey(m => m.BoardMeetingLocationID)
                .WillCascadeOnDelete(true);
        }
    }
}