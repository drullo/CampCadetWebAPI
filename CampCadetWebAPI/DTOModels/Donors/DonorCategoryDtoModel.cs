namespace CampCadetWebAPI.DTOModels
{
    public class DonorCategoryDtoModel
    {
        public int? ID { get; set; }

        public string Description { get; set; }

        public bool? Enabled { get; set; }

        public int? SortPriority { get; set; }

        public bool? ShowDonorLevels { get; set; }
    }
}