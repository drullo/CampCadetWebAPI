using CampCadetWebAPI.DTOModels;
using System.Collections.Generic;

namespace CampCadetWebAPI.Models
{
    public class LinkCategoryModel : LinkCategoryDtoModel
    {
        public List<LinkModel> Links { get; set; }
    }
}