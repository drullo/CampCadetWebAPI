namespace CampCadetWebAPI.Models
{
    public class DonorLevelModel
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public decimal AmountLower { get; set; }

        public string Color { get; set; }

        public decimal AmountUpper { get; set; }

        public string FontColor { get; set; }
    }
}