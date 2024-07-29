namespace MEJORA.Application.Dtos.UserPerson.Response
{
    public class GetUserPersonByEmailResponse
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Subscription_Plan_Id { get; set; }
        public int CountryId { get; set; }
        public int Status { get; set; }
        public string? State { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }
        public bool? State_Validation_Email { get; set; }
        public string? Guid_id { get; set; }
        public string? Phone_number { get; set; }
        public bool? Is_Admin { get; set; }
    }
}
