using CampCadetWebAPI.Models;

namespace CampCadetWebAPI.DTOModels
{
    public class MemberDtoModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Prefix { get; set; }

        public string Email { get; set; }

        public int? SortPriority { get; set; }

        public bool? Enabled { get; set; }

        public bool? ShowEmail { get; set; }

        public MemberCategoryDtoModel BoardMemberCategory { get; set; }
    }
}