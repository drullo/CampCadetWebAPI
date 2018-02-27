using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class CampSupplyConfiguration : EntityTypeConfiguration<CampSupplyModel>
    {
        public CampSupplyConfiguration()
        {
            ToTable("CampSupplies");

            Property(s => s.Item)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_SupplyItem") { IsUnique = true }
                    )
                );
        }
    }
}