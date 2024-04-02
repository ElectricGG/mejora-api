namespace MEJORA.Application.Dtos.UserPerson.Response
{
    public class GetUserPersonByUsernameResponse
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? SubscriptionPlanId { get; set; }
        public int CountryId { get; set; }
        public int Status { get; set; }
        public string? State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
