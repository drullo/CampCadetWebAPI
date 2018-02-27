namespace CampCadetWebAPI.DTOModels
{
    public class DonorDtoModel
    {
        public int? ID { get; set; }

        public string Name { get; set; }

        public bool? DisplayOnWebsite { get; set; }

        public string Notes { get; set; }

        public int? SortPriority { get; set; }

        public string Website { get; set; }

        public string IconURL { get; set; }
    }
}