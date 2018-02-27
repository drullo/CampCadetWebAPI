using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class MemberCategoryConfiguration : EntityTypeConfiguration<MemberCategoryModel>
    {
        public MemberCategoryConfiguration()
        {
            ToTable("BoardMemberCategories");

            Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_MemberCategoryDescription") { IsUnique = true }
                    )
                );

            HasMany(c => c.BoardMembers)
                .WithRequired(m => m.BoardMemberCategory)
                .HasForeignKey(m => m.BoardMemberCategoryID)
                .WillCascadeOnDelete(true);
        }
    }
}