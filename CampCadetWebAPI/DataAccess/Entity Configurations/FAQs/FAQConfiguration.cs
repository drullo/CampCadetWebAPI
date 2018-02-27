using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class FAQConfiguration : EntityTypeConfiguration<FAQModel>
    {
        public FAQConfiguration()
        {
            ToTable("FAQs");

            Property(f => f.Question)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_FAQQuestion") { IsUnique = true }
                    )
                );

            Property(f => f.Answer)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}