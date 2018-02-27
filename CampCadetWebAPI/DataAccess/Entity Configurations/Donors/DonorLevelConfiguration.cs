using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class DonorLevelConfiguration : EntityTypeConfiguration<DonorLevelModel>
    {
        public DonorLevelConfiguration()
        {
            ToTable("DonorLevels");

            Property(l => l.Description)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_DonorLevelDescription") { IsUnique = true }
                    )
                );

            Property(l => l.Color)
                .IsRequired()
                .HasMaxLength(10);

            Property(l => l.FontColor)
                .IsRequired()
                .HasMaxLength(10);

            Property(l => l.AmountLower)
                .HasColumnType("money");

            Property(l => l.AmountUpper)
                .HasColumnType("money");
        }
    }
}