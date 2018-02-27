using CampCadetWebAPI.DTOModels;
using System.Collections.Generic;

namespace CampCadetWebAPI.Models
{
    public class MemberCategoryModel : MemberCategoryDtoModel
    {
        public List<MemberModel> BoardMembers { get; set; }
    }
}