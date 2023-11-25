using ThAmCo.User_Profiles.Models;

namespace ThAmCo.User_Profiles.Repositories.Repository.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetAllCustomersFromDatabase();
        public User GetUserByIdFromDatabase(string userId);
        public User GetUserByUsernameAndEmailFromDatabase(string username, string email);
        public int AddNewUserToDatabase(User userToAdd);
        public bool UpdateUserToDatabase();
        public int DeleteUserFromDatabase(User userToRemove);
        public bool UpdateCustomerFundsToDatabase();
    }
}
