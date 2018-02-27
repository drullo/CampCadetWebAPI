using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class ContactReasonCategoryConfiguration : EntityTypeConfiguration<ContactReasonCategoryModel>
    {
        public ContactReasonCategoryConfiguration()
        {
            ToTable("ContactReasonCategories");

            Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_ContactReasonCategoryDescription") { IsUnique = true }
                    )
                );
        }
    }
}