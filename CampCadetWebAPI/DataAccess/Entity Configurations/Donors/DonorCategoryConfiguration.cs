using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class DonorCategoryConfiguration : EntityTypeConfiguration<DonorCategoryModel>
    {
        public DonorCategoryConfiguration()
        {
            ToTable("DonorCategories");

            Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_DonorCategoryDescription") { IsUnique = true }
                    )
                );

            HasMany(c => c.Donations)
                .WithRequired(c => c.DonorCategory)
                .HasForeignKey(c => c.CategoryID)
                .WillCascadeOnDelete(true);
        }
    }
}