using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class EligibilityRequirementConfiguration : EntityTypeConfiguration<EligibilityRequirementModel>
    {
        public EligibilityRequirementConfiguration()
        {
            ToTable("EligibilityRequirements");

            Property(r => r.Requirement)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_EligibilityRequirement") { IsUnique = true }
                    )
                );
        }
    }
}