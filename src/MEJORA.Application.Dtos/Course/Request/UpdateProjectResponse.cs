namespace MEJORA.Application.Dtos.Course.Request
{
    public class UpdateProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MediaCount { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string HashedId { get; set; }
        public bool Public { get; set; }
        public string PublicId { get; set; }
        public bool AnonymousCanUpload { get; set; }
        public bool AnonymousCanDownload { get; set; }
    }
}
