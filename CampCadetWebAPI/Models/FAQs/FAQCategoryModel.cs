using CampCadetWebAPI.DTOModels;
using System.Collections.Generic;

namespace CampCadetWebAPI.Models
{
    public class FAQCategoryModel : FAQCategoryDtoModel
    {
        public List<FAQModel> FAQs { get; set; }
    }
}