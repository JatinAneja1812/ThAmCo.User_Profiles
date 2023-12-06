using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using ThAmCo.User_Profiles.Automapper;
using ThAmCo.User_Profiles.Enums;
using ThAmCo.User_Profiles.Models;
using ThAmCo.User_Profiles.Repositories.Repository.Interfaces;
using ThAmCo.User_Profiles.Services.Service.Classes;
using ThAmCo.User_Profiles.Utility;
using Xunit;

namespace UserProfilesService.Tests
{
    public class GetStaffDataTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ILogger<UserService>> _logger;
        private readonly Mock<IGuidUtility> _guidUtility;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public GetStaffDataTest()
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
        public void GetStaffData_Should_Return_Null_If_Staff_Not_Found()
        {
            // Arrange
            string username = "nonexistent_user";
            string email = "nonexistent_email@example.com";
            _userRepository.Setup(repo => repo.GetUserByUsernameAndEmailFromDatabase(username, email)).Returns((User)null);

            // Act
            var result = _userService.GetStaffData(email);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetStaffData_Should_Return_Staff_Details_If_Found()
        {
            // Arrange
            string email = "existing_email@example.com";
            var existingStaff = new User 
            { 
                UserId = Guid.NewGuid().ToString(),
                Username = "existing_user",
                Email = "existing_email@example.com",
                FirstName = "Elli",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                AvailableFunds = 500.00,
                UserAddedOnDate = DateTime.UtcNow,
                UserType = UserTypeEnum.Customer,
                LocationNumber = "123",
                Street = "Main Street",
                City = "Cityville",
                State = "United Kingdoms",
                PostalCode = "TS64KU"
            };

            _userRepository.Setup(repo => repo.GetUserByEmailFromDatabase(email)).Returns(existingStaff);

            // Act
            var result = _userService.GetStaffData(email);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetStaffData_Should_Return_Null_If_Exception_Occurs()
        {
            // Arrange
            string username = "error_user";
            string email = "error_email@example.com";
            _userRepository.Setup(repo => repo.GetUserByEmailFromDatabase(email)).Throws(new Exception("Simulated error"));

            // Act
            var result = _userService.GetStaffData(email);

            // Assert
            Assert.Null(result);
        }
    }
}
