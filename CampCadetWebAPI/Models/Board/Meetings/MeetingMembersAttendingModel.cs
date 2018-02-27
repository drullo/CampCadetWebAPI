namespace CampCadetWebAPI.Models
{
    public class MeetingMembersAttendingModel
    {
        //public int ID { get; set; }

        public int BoardMeetingID { get; set; }
        public MeetingModel BoardMeeting { get; set; }

        public int BoardMemberID { get; set; }
        public MemberModel BoardMember { get; set; }
    }
}