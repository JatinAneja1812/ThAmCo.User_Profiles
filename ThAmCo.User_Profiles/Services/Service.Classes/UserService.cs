using AutoMapper;
using Exceptions;
using ThAmCo.User_Profiles.Controllers;
using ThAmCo.User_Profiles.DTOs;
using ThAmCo.User_Profiles.Enums;
using ThAmCo.User_Profiles.Models;
using ThAmCo.User_Profiles.Repositories.Repository.Interfaces;
using ThAmCo.User_Profiles.Services.Service.Interfaces;

namespace ThAmCo.User_Profiles.Services.Service.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserProfilesController> _logger;
        private readonly IMapper _mapper;
        public UserService(IUserRepository UserRepository, ILogger<UserProfilesController> Logger, IMapper Mapper)
        {
            _userRepository = UserRepository;
            _logger = Logger;
            _mapper = Mapper;
        }

        public List<UserProfilesDTO> GetAllCustomser()
        {
            try
            {
                List<User> allCustomers = _userRepository.GetAllCustomersFromDatabase();

                if (allCustomers == null) // no user found
                {
                    return new List<UserProfilesDTO>();
                }

                List<UserProfilesDTO> userProfilesDTOs = _mapper.Map<List<User>, List<UserProfilesDTO>>(allCustomers);

                return userProfilesDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId((int)LogEventIdEnum.GetFailed), $"Failed to retrieve customers from the database. \nError occured in User Service at GetAllCustomser(...) with following error message and stack trace." +
                 $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}");

                return null;
            }
        }

        public UserProfilesDTO GetStaffData(string username, string email)
        {
            try
            {
                User existingStaff = _userRepository.GetUserByUsernameAndEmailFromDatabase(username, email);

                if (existingStaff == null)
                {
                    return null;
                }

                UserProfilesDTO staffDetails = _mapper.Map<User, UserProfilesDTO>(existingStaff);

                return staffDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId((int)LogEventIdEnum.GetFailed), $"Failed to retrieve customers from the database. \nError occured in User Service at GetAllCustomser(...) with following error message and stack trace." +
                 $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}");

                return null;
            }
        }

        public bool AddNewUser(UserProfilesDTO userDataToAdd)
        {
            try
            {
                User existingUser = _userRepository.GetUserByUsernameAndEmailFromDatabase(userDataToAdd.Username, userDataToAdd.Email);

                if (existingUser != null)
                {
                    throw new UserExistsException();
                }

                User newUser = _mapper.Map<UserProfilesDTO, User>(userDataToAdd);
                newUser.UserId = Guid.NewGuid();
                newUser.UserAddedOnDate = DateTime.Now;
                newUser.AvailableFunds = 0.00;

                int didSave = _userRepository.AddNewUserToDatabase(newUser);

                return didSave > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId((int)LogEventIdEnum.InsertFailed), $"Failed to add new customers to the database. \nError occured in User Service at AddNewCustomer(...) with following error message and stack trace." +
                 $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}");

                return false;
            }
        }

        public bool UpdateUser(UserProfilesDTO userDataToUpdate)
        {
            try
            {
                User existingUser = _userRepository.GetUserByIdFromDatabase(userDataToUpdate.UserId.ToString()) ?? throw new DataNotFoundException();
                // Update user properties based on the provided DTO
                existingUser.Username = userDataToUpdate.Username;
                existingUser.Email = userDataToUpdate.Email;
                existingUser.FirstName = userDataToUpdate.FirstName;
                existingUser.LastName = userDataToUpdate.LastName;
                existingUser.PhoneNumber = userDataToUpdate.PhoneNumber;
                existingUser.LocationNumber = userDataToUpdate.LocationNumber;
                existingUser.Street = userDataToUpdate.Street;
                existingUser.City = userDataToUpdate.City;
                existingUser.State = userDataToUpdate.State;
                existingUser.PostalCode = userDataToUpdate.PostalCode;

                int didUpdate = _userRepository.UpdateUserToDatabase(existingUser);

                return didUpdate > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId((int)LogEventIdEnum.InsertFailed), $"Failed to add new customers to the database. \nError occured in User Service at UpdateUser(...) with following error message and stack trace." +
                 $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}");

                return false;
            }
        }

        public bool DeleteUser(string userId)
        {
            try
            {
                User existsingUser = _userRepository.GetUserByIdFromDatabase(userId) ?? throw new DataNotFoundException();

                int didRemove = _userRepository.DeleteUserFromDatabase(existsingUser);

                return didRemove > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId((int)LogEventIdEnum.InsertFailed), $"Failed to add new customers to the database. \nError occured in User Service at DeleteUser(...) with following error message and stack trace." +
                 $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}");

                return false;
            }
        }

        public bool UpdateCustomerFunds(CustomerFundsDTO updatedCustomerFunds)
        {
            try
            {
                User existingUser = _userRepository.GetUserByIdFromDatabase(updatedCustomerFunds.UserId) ?? throw new DataNotFoundException();
                // Update user properties based on the provided DTO
                existingUser.AvailableFunds = updatedCustomerFunds.Amount;

                int didUpdate = _userRepository.UpdateUserToDatabase(existingUser);

                return didUpdate > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId((int)LogEventIdEnum.InsertFailed), $"Failed to update customers funds to the database. \nError occured in User Service at UpdateCustomerFunds(...) with following error message and stack trace." +
                 $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}");

                return false;
            }
        }
    }
}
