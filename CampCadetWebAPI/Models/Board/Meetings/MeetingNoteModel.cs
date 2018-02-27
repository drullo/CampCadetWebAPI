namespace CampCadetWebAPI.Models
{
    public class MeetingNoteModel
    {
        public int ID { get; set; }

        public string Topic { get; set; }

        public string Notes { get; set; }

        public bool PublicInfo { get; set; }

        public int BoardMeetingID { get; set; }
        public MeetingModel BoardMeeting { get; set; }
    }
}
