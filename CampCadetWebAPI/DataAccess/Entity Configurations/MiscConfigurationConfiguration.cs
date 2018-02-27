using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class MiscConfigurationConfiguration : EntityTypeConfiguration<MiscConfigurationModel>
    {
        public MiscConfigurationConfiguration()
        {
            ToTable("MiscConfiguration");

            Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_ConfigurationDescription") { IsUnique = true }
                    )
                );
        }
    }
}