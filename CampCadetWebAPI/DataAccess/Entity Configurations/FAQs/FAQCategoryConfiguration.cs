using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class FAQCategoryConfiguration : EntityTypeConfiguration<FAQCategoryModel>
    {
        public FAQCategoryConfiguration()
        {
            ToTable("FAQCategories");

            Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_FAQCategoryDescription") { IsUnique = true }
                    )
                );

            HasMany(c => c.FAQs)
                .WithRequired(f => f.FAQCategory)
                .HasForeignKey(f => f.FAQCategoryID)
                .WillCascadeOnDelete(true);
        }
    }
}