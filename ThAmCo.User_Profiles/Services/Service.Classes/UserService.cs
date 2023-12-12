using AutoMapper;
using Exceptions;
using ThAmCo.User_Profiles.DTOs;
using ThAmCo.User_Profiles.Enums;
using ThAmCo.User_Profiles.Models;
using ThAmCo.User_Profiles.Repositories.Repository.Interfaces;
using ThAmCo.User_Profiles.Services.Service.Interfaces;
using ThAmCo.User_Profiles.Utility;

namespace ThAmCo.User_Profiles.Services.Service.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGuidUtility _guidUtility;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        public UserService(IUserRepository UserRepository, IGuidUtility GuidUtility , ILogger<UserService> Logger, IMapper Mapper)
        {
            _userRepository = UserRepository;
            _guidUtility = GuidUtility;
            _logger = Logger;
            _mapper = Mapper;
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
                newUser.UserId = _guidUtility.GenerateShortGuid(Guid.NewGuid());
                newUser.UserAddedOnDate = DateTime.Now;
                newUser.AvailableFunds = 0.00;

                int didSave = _userRepository.AddNewUserToDatabase(newUser);

                return didSave > 0;
            }
            catch (UserExistsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId((int)LogEventIdEnum.InsertFailed), $"Failed to add new customers to the database. \nError occured in User Service at AddNewCustomer(...) with following error message and stack trace." +
                 $"{ex.Message}\n{ex.StackTrace}\nInner exception: {(ex.InnerException != null ? ex.InnerException.Message + "\n" + ex.InnerException.StackTrace : "None")}");

                return false;
            }
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

                throw;
            }
        }

        public UserProfilesDTO GetStaffData(string email)
        {
            try
            {
                User existingStaff = _userRepository.GetUserByEmailFromDatabase(email);

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
            catch (DataNotFoundException)
            {
                throw;
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
            catch (DataNotFoundException)
            {
                throw;
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
                existingUser.AvailableFunds = existingUser.AvailableFunds + updatedCustomerFunds.Amount;

                int didUpdate = _userRepository.UpdateUserToDatabase(existingUser);

                return didUpdate > 0;
            }
            catch (DataNotFoundException)
            {
                throw;
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
