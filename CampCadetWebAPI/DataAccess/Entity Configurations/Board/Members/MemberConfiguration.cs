using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class MemberConfiguration : EntityTypeConfiguration<MemberModel>
    {
        public MemberConfiguration()
        {
            ToTable("BoardMembers");

            Property(m => m.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_MemberName", 0) { IsUnique = true }
                    )
                );

            Property(m => m.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_MemberName", 1) { IsUnique = true }
                    )
                );

            Property(m => m.Title)
                .HasMaxLength(250);

            Property(m => m.Description)
                .HasMaxLength(500);

            Property(m => m.Prefix)
                .HasMaxLength(50);

            Property(m => m.Email)
                .HasMaxLength(100);

            HasMany(m => m.BoardMeetings)
                .WithRequired(m => m.BoardMember)
                .HasForeignKey(m => m.BoardMemberID)
                .WillCascadeOnDelete(true);

            /*HasRequired(m => m.BoardCategory)
                .WithRequiredDependent()
                .Map(m => m.MapKey("BoardMemberCategory_ID"));*/
        }
    }
}