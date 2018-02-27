using CampCadetWebAPI.DTOModels;

namespace CampCadetWebAPI.Models
{
    public class DonorCategoryLinkModel : DonorCategoryLinkDtoModel
    {
        public int DonorID { get; set; }
        public new DonorModel Donor { get; set; }

        public int CategoryID { get; set; }
        public new DonorCategoryModel DonorCategory { get; set; }
    }
}