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

                if (allCustomers == null)
                {
                    return null;
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
                User exisingStaff = _userRepository.GetUserByUsernameAndEmailFromDatabase(username, email);

                if (exisingStaff == null)
                {
                    return null;
                }

                UserProfilesDTO staffDetails = _mapper.Map<User, UserProfilesDTO>(exisingStaff);

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
                User existsingUser = _userRepository.GetUserByUsernameAndEmailFromDatabase(userDataToAdd.Username, userDataToAdd.Email);

                if (existsingUser != null)
                {
                    throw new UserExistsException();
                }

                User newUser = _mapper.Map<UserProfilesDTO, User>(userDataToAdd);
                newUser.UserId = Guid.NewGuid();
                newUser.UserAddedOnDate = DateTime.Now;
                newUser.AvailableFunds = 0.00;

                int didSave = _userRepository.AddNewUserToDatabase(newUser);

                if (didSave == -1)
                {
                    return false;
                }
                return true;
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
            throw new NotImplementedException();
        }

        public bool DeleteUser(string userId)
        {
            try
            {
                User existsingUser = _userRepository.GetUserByIdFromDatabase(userId) ?? throw new DataNotFoundException();

                int didRemove = _userRepository.DeleteUserFromDatabase(existsingUser);

                if (didRemove == -1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId((int)LogEventIdEnum.InsertFailed), $"Failed to add new customers to the database. \nError occured in User Service at AddNewCustomer(...) with following error message and stack trace." +
                 $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}");

                return false;
            }
        }

        public bool UpdateCustomerFunds(string userId, double amount)
        {
            throw new NotImplementedException();
        }
    }
}
