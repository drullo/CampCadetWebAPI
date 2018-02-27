using System;

namespace CampCadetWebAPI.DTOModels
{
    public class MeetingDtoModel
    {
        public int ID { get; set; }

        public DateTime DateTime { get; set; }

        public string Notes { get; set; }

        public MeetingLocationDtoModel BoardMeetingLocation { get; set; }
    }
}