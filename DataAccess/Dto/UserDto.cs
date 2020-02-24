using System;

namespace DataAccess.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public int HuntsmanId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailVerified { get; set; }
        public string ActivationCode { get; set; }
        public DateTime ActivationCodeTimeStamp { get; set; }
        public DateTime CreationTimeStamp { get; set; }
    }
}
