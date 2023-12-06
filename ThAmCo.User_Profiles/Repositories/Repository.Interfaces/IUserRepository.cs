using ThAmCo.User_Profiles.Models;

namespace ThAmCo.User_Profiles.Repositories.Repository.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetAllCustomersFromDatabase();
        public User GetUserByIdFromDatabase(string userId);
        public User GetUserByUsernameAndEmailFromDatabase(string username, string email);
        public User GetUserByEmailFromDatabase(string email);
        public int AddNewUserToDatabase(User userToAdd);
        public int UpdateUserToDatabase(User userToUpdate);
        public int DeleteUserFromDatabase(User userToRemove);
    }
}
