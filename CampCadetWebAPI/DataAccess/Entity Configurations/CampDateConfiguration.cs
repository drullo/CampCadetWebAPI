using CampCadetWebAPI.Models;
using System.Data.Entity.ModelConfiguration;

namespace CampCadetWebAPI.DataAccess.Entity_Configurations
{
    public class CampDateConfiguration : EntityTypeConfiguration<CampDateModel>
    {
        public CampDateConfiguration()
        {
            ToTable("CampDates");

            HasKey(d => d.StartDate);
        }
    }
}