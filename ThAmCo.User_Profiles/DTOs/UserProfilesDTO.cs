using ThAmCo.User_Profiles.Enums;

namespace ThAmCo.User_Profiles.DTOs
{
    public class UserProfilesDTO
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public double AvailableFunds { get; set; }
        public UserTypeEnum UserType { get; set; }
        public string LocationNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public DateTime UserAddedOnDate { get; set; }
    }
}
