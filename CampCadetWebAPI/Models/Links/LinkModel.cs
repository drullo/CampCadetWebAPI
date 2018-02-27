using CampCadetWebAPI.DTOModels;

namespace CampCadetWebAPI.Models
{
    public class LinkModel : LinkDtoModel
    {
        public int LinkCategoryID { get; set; }
        public new LinkCategoryModel LinkCategory { get; set; }
    }
}