using CampCadetWebAPI.Models;

namespace CampCadetWebAPI.DTOModels
{
    public class FAQDtoModel
    {
        public int? ID { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public FAQCategoryDtoModel FAQCategory { get; set; }
    }
}