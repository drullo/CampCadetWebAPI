namespace CampCadetWebAPI.DTOModels
{
    public class LinkDtoModel
    {
        public int? ID { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }

        public LinkCategoryDtoModel LinkCategory { get; set; }
    }
}