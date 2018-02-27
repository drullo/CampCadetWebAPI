using CampCadetWebAPI.DTOModels;
using System.Collections.Generic;

namespace CampCadetWebAPI.Models
{
    public class MeetingLocationModel : MeetingLocationDtoModel
    {
        public List<MeetingModel> BoardMeetings { get; set; }
    }
}