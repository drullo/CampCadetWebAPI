using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class LinkCategoryConfiguration : EntityTypeConfiguration<LinkCategoryModel>
    {
        public LinkCategoryConfiguration()
        {
            ToTable("LinkCategories");

            Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_LinkCategoryDescription") { IsUnique = true }
                    )
                );

            HasMany(c => c.Links)
                .WithRequired(l => l.LinkCategory)
                .HasForeignKey(l => l.LinkCategoryID)
                .WillCascadeOnDelete(true);
        }
    }
}