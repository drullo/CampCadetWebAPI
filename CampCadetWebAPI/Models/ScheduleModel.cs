using System;

namespace CampCadetWebAPI.Models
{
    public class ScheduleModel
    {
        public int ID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Event { get; set; }

        public string InfoURL { get; set; }

        public string AdditionalInfo { get; set; }
    }
}