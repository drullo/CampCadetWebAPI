using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class CampRuleConfiguration : EntityTypeConfiguration<CampRuleModel>
    {
        public CampRuleConfiguration()
        {
            ToTable("CampRules");

            Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_RuleDescription") { IsUnique = true }
                    )
                );
        }
    }
}