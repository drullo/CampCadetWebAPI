namespace CampCadetWebAPI.DTOModels
{
    public class DonorCategoryLinkDtoModel
    {
        public DonorDtoModel Donor { get; set; }

        public DonorCategoryDtoModel DonorCategory { get; set; }

        public decimal? AmountGiven { get; set; }

        public string Notes { get; set; }
    }
}