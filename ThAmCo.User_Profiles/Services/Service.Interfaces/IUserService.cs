using ThAmCo.User_Profiles.DTOs;

namespace ThAmCo.User_Profiles.Services.Service.Interfaces
{
    public interface IUserService
    {
       
        // Retrieve customer 
        List<UserProfilesDTO> GetAllCustomser();
        
        // Retrieve current logged in staff
        UserProfilesDTO GetStaffData(string username, string email);

        // Added a new customer
        public bool AddNewUser(UserProfilesDTO userDataToAdd);

        // Update customer details
        public bool UpdateUser(UserProfilesDTO userDataToUpdate);

        // Delete customer by ID
        public bool DeleteUser(string userId);

        // Udpate customer available funds
        public bool UpdateCustomerFunds(CustomerFundsDTO updatedCustomerFunds);
    }
}
