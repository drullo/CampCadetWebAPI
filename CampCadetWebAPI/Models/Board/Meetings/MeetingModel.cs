using CampCadetWebAPI.DTOModels;
using System.Collections.Generic;

namespace CampCadetWebAPI.Models
{
    public class MeetingModel : MeetingDtoModel
    {
        public int BoardMeetingLocationID { get; set; }
        public new MeetingLocationModel BoardMeetingLocation { get; set; }

        public List<MeetingNoteModel> BoardMeetingNotes { get; set; }

        public List<MeetingMembersAttendingModel> BoardMeetingMembers { get; set; }
    }
}