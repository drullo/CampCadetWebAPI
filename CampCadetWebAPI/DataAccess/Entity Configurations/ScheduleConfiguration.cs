using CampCadetWebAPI.Models;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class ScheduleConfiguration : EntityTypeConfiguration<ScheduleModel>
    {
        public ScheduleConfiguration()
        {
            ToTable("Schedule");

            Property(s => s.Event)
                .IsRequired()
                .HasMaxLength(500);

            Property(s => s.InfoURL)
                .HasMaxLength(500);
        }
    }
}