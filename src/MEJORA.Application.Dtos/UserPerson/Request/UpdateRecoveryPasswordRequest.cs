namespace MEJORA.Application.Dtos.UserPerson.Request
{
    public class UpdateRecoveryPasswordRequest
    {
        public int UserPersonId { get; set; }
        public string Password { get; set; }
    }
}
