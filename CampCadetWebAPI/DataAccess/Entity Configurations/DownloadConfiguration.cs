using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class DownloadConfiguration : EntityTypeConfiguration<DownloadModel>
    {
        public DownloadConfiguration()
        {
            ToTable("Downloads");

            Property(d => d.FileName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_DownloadFileName") { IsUnique = true }
                    )
                ); ;

            Property(d => d.UploadedBy)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}