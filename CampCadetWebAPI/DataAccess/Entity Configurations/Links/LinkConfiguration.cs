using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class LinkConfiguration : EntityTypeConfiguration<LinkModel>
    {
        public LinkConfiguration()
        {
            ToTable("Links");

            Property(l => l.Description)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_LinkDescription") { IsUnique = true }
                    )
                );

            Property(l => l.URL)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}