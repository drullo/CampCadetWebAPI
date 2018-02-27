using CampCadetWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class DonorCategoryLinkConfiguration : EntityTypeConfiguration<DonorCategoryLinkModel>
    {
        public DonorCategoryLinkConfiguration()
        {
            ToTable("DonorCategoryLinks");

            HasKey(l => new { l.DonorID, l.CategoryID });

            /*Property(l => l.DonorID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(l => l.CategoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);*/

            Property(l => l.AmountGiven)
                .HasColumnType("money");

            Property(l => l.Notes)
                .HasMaxLength(1000);
        }
    }
}