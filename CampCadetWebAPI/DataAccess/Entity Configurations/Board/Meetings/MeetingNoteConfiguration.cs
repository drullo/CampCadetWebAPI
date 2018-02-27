using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class MeetingNoteConfiguration : EntityTypeConfiguration<MeetingNoteModel>
    {
        public MeetingNoteConfiguration()
        {
            ToTable("BoardMeetingNotes");

            Property(n => n.Topic)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}