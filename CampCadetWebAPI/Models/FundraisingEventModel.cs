using System;

namespace CampCadetWebAPI.Models
{
    public class FundraisingEventModel
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Details { get; set; }
    }
}