namespace CampCadetWebAPI.Models
{
    public class DownloadModel
    {
        public int ID { get; set; }

        public string FileName { get; set; }

        public bool AllowDownload { get; set; }

        public string UploadedBy { get; set; }
    }
}