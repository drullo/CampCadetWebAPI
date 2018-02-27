using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class DonorConfiguration : EntityTypeConfiguration<DonorModel>
    {
        public DonorConfiguration()
        {
            ToTable("Donors");

            Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_DonorName") { IsUnique = true }
                    )
                );

            Property(d => d.Website)
                .HasMaxLength(250);

            Property(d => d.IconURL)
                .HasMaxLength(250);

            HasMany(d => d.Donations)
                .WithRequired(d => d.Donor)
                .HasForeignKey(d => d.DonorID)
                .WillCascadeOnDelete(true);
        }
    }
}