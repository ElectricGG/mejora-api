namespace MEJORA.Application.Dtos.Wistia.Response
{
    public class UploadMediaResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Archived { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public double Duration { get; set; }
        public string Hashed_Id { get; set; }
        public string Description { get; set; }
        public double Progress { get; set; }
        public string Status { get; set; }
        public Thumbnail Thumbnail { get; set; }
        public int AccountId { get; set; }
    }

    public class Thumbnail
    {
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
