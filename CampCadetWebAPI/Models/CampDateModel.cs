using System;

namespace CampCadetWebAPI.Models
{
    public class CampDateModel
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? ApplicationDeadline { get; set; }

        public DateTime? ApplicationsAvailableBeginning { get; set; }

        public DateTime? OrientationDate { get; set; }
    }
}