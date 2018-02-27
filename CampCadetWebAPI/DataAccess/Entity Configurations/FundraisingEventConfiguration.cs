using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class FundraisingEventConfiguration : EntityTypeConfiguration<FundraisingEventModel>
    {
        public FundraisingEventConfiguration()
        {
            ToTable("FundraisingEvents");

            Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_EventDescription") { IsUnique = true }
                    )
                );
        }
    }
}