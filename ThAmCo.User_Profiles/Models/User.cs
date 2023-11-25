using System.ComponentModel.DataAnnotations;
using ThAmCo.User_Profiles.Enums;

namespace ThAmCo.User_Profiles.Models
{
    public class User
    {
        private DateTime _userAddedOnDate;

        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public double AvailableFunds { get; set; }
        public DateTime UserAddedOnDate
        {
            get => _userAddedOnDate;

            set => _userAddedOnDate = value.ToUniversalTime();
        }
        public UserTypeEnum UserType { get; set; }
        public string? LocationNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}
