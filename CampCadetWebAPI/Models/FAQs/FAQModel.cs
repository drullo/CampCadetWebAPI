using CampCadetWebAPI.DTOModels;

namespace CampCadetWebAPI.Models
{
    public class FAQModel : FAQDtoModel
    {
        public int FAQCategoryID { get; set; }
        public new FAQCategoryModel FAQCategory { get; set; }
    }
}
