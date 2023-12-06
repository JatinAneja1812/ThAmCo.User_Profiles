using AutoMapper;
using Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using ThAmCo.User_Profiles.Automapper;
using ThAmCo.User_Profiles.DTOs;
using ThAmCo.User_Profiles.Enums;
using ThAmCo.User_Profiles.Models;
using ThAmCo.User_Profiles.Repositories.Repository.Interfaces;
using ThAmCo.User_Profiles.Services.Service.Classes;
using ThAmCo.User_Profiles.Utility;
using Xunit;

namespace UserProfilesService.Tests
{
    public class AddNewUserTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ILogger<UserService>> _logger;
        private readonly Mock<IGuidUtility> _guidUtility;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public AddNewUserTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<UserService>>();
            _guidUtility = new Mock<IGuidUtility>();
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserDataMappingProfile>();
            }));

            _userService = new UserService(_userRepository.Object, _guidUtility.Object, _logger.Object, _mapper);
        }

        [Fact]
        public void AddNewUser_Should_Throw_UserExistsException_If_User_Already_Exists()
        {
            // Arrange
            var userDataToAdd = new UserProfilesDTO
            {
                Username = "john_doe",
                Email = "john.doe@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                UserType = UserTypeEnum.Customer,
                LocationNumber = "123",
                Street = "Main Street",
                City = "Cityville",
                State = "United Kingdoms",
                PostalCode = "TS64KU",
            };

            var existinguser = new User
            {
                Username = "john_doe",
                Email = "john.doe@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                UserType = UserTypeEnum.Customer,
                LocationNumber = "123",
                Street = "Main Street",
                City = "Cityville",
                State = "United Kingdoms",
                PostalCode = "TS64KU",
            };

            _userRepository.Setup(repo => repo.GetUserByUsernameAndEmailFromDatabase(It.IsAny<string>(), It.IsAny<string>())).Returns(existinguser);

            // Act  
            var exception = Record.Exception(() => _userService.AddNewUser(userDataToAdd));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UserExistsException>(exception);
        }

        [Fact]
        public void AddNewUser_Should_Save_New_User_And_Return_True_If_User_Does_Not_Exist()
        {
            // Arrange
            var userDataToAdd = new UserProfilesDTO
            {
                UserId = Guid.NewGuid().ToString(),
                Username = "jane_danne",
                Email = "jane.danne123@example.com",
                FirstName = "Jane",
                LastName = "Danne",
                PhoneNumber = "76345643290",
                UserType = UserTypeEnum.Customer,
                LocationNumber = "133",
                AvailableFunds = 0,
                UserAddedOnDate = DateTime.Now,
                Street = "Main Street",
                City = "Cityville",
                State = "United Kingdoms",
                PostalCode = "TS64KU"
            };

            _userRepository.Setup(repo => repo.GetUserByUsernameAndEmailFromDatabase(It.IsAny<string>(), It.IsAny<string>())).Returns((User)null);
            _userRepository.Setup(repo => repo.AddNewUserToDatabase(It.IsAny<User>())).Returns(1); // Assuming a successful save

            // Act
            var result = _userService.AddNewUser(userDataToAdd);

            // Assert
            _userRepository.Verify(repo => repo.AddNewUserToDatabase(It.IsAny<User>()), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void AddNewUser_Should_Return_False_If_Save_Fails()
        {
            // Arrange
            var userDataToAdd = new UserProfilesDTO
            {
                UserId = Guid.NewGuid().ToString(),
                Username = "jane_danne",
                Email = "jane.danne123@example.com",
                FirstName = "Jane",
                LastName = "Danne",
                PhoneNumber = "76345643290",
                UserType = UserTypeEnum.Customer,
                LocationNumber = "133",
                AvailableFunds = 0,
                UserAddedOnDate = DateTime.Now,
                Street = "Main Street",
                City = "Cityville",
                State = "United Kingdoms",
                PostalCode = "TS64KU"
            };

            _userRepository.Setup(repo => repo.GetUserByUsernameAndEmailFromDatabase(It.IsAny<string>(), It.IsAny<string>())).Returns((User)null);
            _userRepository.Setup(repo => repo.AddNewUserToDatabase(It.IsAny<User>())).Returns(0); // Assuming a failed save

            // Act
            var result = _userService.AddNewUser(userDataToAdd);

            // Assert
            _userRepository.Verify(repo => repo.AddNewUserToDatabase(It.IsAny<User>()), Times.Once);
            Assert.False(result);
        }

        [Fact]
        public void AddNewUser_Should_Return_False_If_Exception_Occurs()
        {
            // Arrange
            var userDataToAdd = new UserProfilesDTO
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = "FirstName",
                Username = "fname_01",
                Email = "name.example@gmail.com",
                LastName = "LastName",
                PhoneNumber = "1234567890",
                LocationNumber = "123",
                Street = "Street",
                City = "City",
                State = "State",
                PostalCode = "OLD12345",
            };

            _userRepository.Setup(repo => repo.GetUserByUsernameAndEmailFromDatabase(userDataToAdd.Username.ToString(), userDataToAdd.Email.ToString())).Throws(new Exception("Simulated error"));

            // Act
            var result = _userService.AddNewUser(userDataToAdd);

            // Assert
            Assert.False(result);
        }
    }
}
