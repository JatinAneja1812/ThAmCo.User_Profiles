using Microsoft.EntityFrameworkCore;
using ThAmCo.User_Profiles.DatabaseContext;
using ThAmCo.User_Profiles.Enums;
using ThAmCo.User_Profiles.Models;
using ThAmCo.User_Profiles.Repositories.Repository.Interfaces;

namespace ThAmCo.User_Profiles.Repositories.Repository.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly ProfilesContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ProfilesContext UserProfilesContext, ILogger<UserRepository> Logger)
        {
            _context = UserProfilesContext;
            _logger = Logger;
        }

        public int AddNewUserToDatabase(User userToAdd)
        {
            try
            {
                _context.Users.Add(userToAdd);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                   new EventId((int)LogEventIdEnum.InsertFailed),
                   $"Failed to add user data with username: {userToAdd.Username} and email: {userToAdd.Email} to the database. Error occurred in Users Repository at AddNewUserToDatabase(...) with the following message and stack trace: " +
                   $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}"
                  );

                return -1;
            }
        }

        public int DeleteUserFromDatabase(User userToRemove)
        {
            try
            {
                _context.Users.Remove(userToRemove);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                   new EventId((int)LogEventIdEnum.InsertFailed),
                   $"Failed to remove user data with username: {userToRemove.Username} and email: {userToRemove.Email} from the database. Error occurred in Users Repository at DeleteUserFromDatabase(...) with the following message and stack trace: " +
                   $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}"
                  );

                return -1;
            }
        }

        public List<User> GetAllCustomersFromDatabase()
        {
            try
            {
                List<User> customers = _context.Users.Where(c => c.UserType == UserTypeEnum.Customer).ToList();
                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                   new EventId((int)LogEventIdEnum.GetFailed),
                   $"Failed to retrive customers account from the database. Error occurred in User Repository at GetAllCustomersFromDatabase(...) with the following message and stack trace: " +
                   $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}"
                  );

                return null;
            }
        }

        public User GetUserByEmailFromDatabase(string email)
        {
            try
            {
                User staffData = _context.Users.Where(c => c.Email == email).First();

                return staffData;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                   new EventId((int)LogEventIdEnum.GetFailed),
                   $"Failed to retrive user with and email: {email} from the database. Error occurred in User Repository at GetUserByEmailFromDatabase(...) with the following message and stack trace: " +
                   $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}"
                  );

                return null;
            }
        }

        public User GetUserByUsernameAndEmailFromDatabase(string username, string email)
        {
            try
            {
                User staffData = _context.Users.Where(c => c.Username == username && c.Email == email).First();

                return staffData;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                   new EventId((int)LogEventIdEnum.GetFailed),
                   $"Failed to retrive user with username: {username} and email: {email} from the database. Error occurred in User Repository at GetUserByUsernameAndEmailFromDatabase(...) with the following message and stack trace: " +
                   $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}"
                  );

                return null;
            }
        }

        public User GetUserByIdFromDatabase(string userId)
        {
            try
            {
                User userData = _context.Users.Where(c => c.UserId == userId).First();

                return userData;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                   new EventId((int)LogEventIdEnum.GetFailed),
                   $"Failed to retrive user with userId: {userId} from the database. Error occurred in User Repository at GetUserByIdFromDatabase(...) with the following message and stack trace: " +
                   $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}"
                  );

                return null;
            }
        }

        public int UpdateUserToDatabase(User userToUpdate)
        {
            try
            {
                _context.Users.Update(userToUpdate);
                return _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(
                    new EventId((int)LogEventIdEnum.UpdateFailed), 
                    $"Failed to update user in the database. \nError occurred in UserRepository at UpdateUserToDatabase(...) with the following " +
                    $"error message and stack trace.\n{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}");

                return -1; // Indicates failure
            }
        }
    }
}
