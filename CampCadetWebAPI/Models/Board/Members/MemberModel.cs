using CampCadetWebAPI.DTOModels;
using System.Collections.Generic;

namespace CampCadetWebAPI.Models
{
    public class MemberModel : MemberDtoModel
    {
        public int BoardMemberCategoryID { get; set; }
        public new MemberCategoryModel BoardMemberCategory { get; set; }

        public List<MeetingMembersAttendingModel> BoardMeetings { get; set; }
    }
}