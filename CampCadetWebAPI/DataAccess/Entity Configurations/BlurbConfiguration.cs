using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class BlurbConfiguration : EntityTypeConfiguration<BlurbModel>
    {
        public BlurbConfiguration()
        {
            ToTable("Blurbs");

            Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_BlurbName") { IsUnique = true }
                    )
                );
        }
    }
}