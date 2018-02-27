using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class ContactCategoryConfiguration : EntityTypeConfiguration<ContactCategoryModel>
    {
        public ContactCategoryConfiguration()
        {
            ToTable("ContactCategories");

            Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_ContactCategoryDescription") { IsUnique = true }
                    )
                );
        }
    }
}