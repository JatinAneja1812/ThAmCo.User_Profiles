using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using ThAmCo.User_Profiles.Automapper;
using ThAmCo.User_Profiles.Enums;
using ThAmCo.User_Profiles.Models;
using ThAmCo.User_Profiles.Repositories.Repository.Interfaces;
using ThAmCo.User_Profiles.Services.Service.Classes;
using Xunit;

namespace UserProfilesService.Tests
{
    public class GetAllCustomersTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ILogger<UserService>> _logger;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public GetAllCustomersTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<UserService>>();
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserDataMappingProfile>();
            }));

            _userService = new UserService(_userRepository.Object, _logger.Object, _mapper);
        }

        [Fact]
        public void GetAllCustomers_Should_Return_EmptyList_If_No_Customers_Found()
        {
            // Arrange
            _userRepository.Setup(repo => repo.GetAllCustomersFromDatabase()).Returns((List<User>)null);

            // Act
            var result = _userService.GetAllCustomser();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllCustomers_Should_Map_Users_To_UserProfilesDTOs()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User
                {
                    UserId = Guid.NewGuid(),
                    Username = "john_doe",
                    Email = "john.doe@example.com",
                    FirstName = "John",
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
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Username = "jane_smith",
                    Email = "jane.smith@example.com",
                    FirstName = "Jane",
                    LastName = "Smith",
                    PhoneNumber = "9876543210",
                    AvailableFunds = 1000.00,
                    UserAddedOnDate = DateTime.UtcNow,
                    UserType = UserTypeEnum.Customer,
                    LocationNumber = "456",
                    Street = "Broadway",
                    City = "Townsville",
                    State = "United Kingdoms",
                    PostalCode = "TS64KU"
                },
            };

            _userRepository.Setup(repo => repo.GetAllCustomersFromDatabase()).Returns(mockUsers);

            // Act
            var result = _userService.GetAllCustomser();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockUsers.Count, result.Count);
            Assert.Equal(mockUsers[0].Username, result[0].Username);
            Assert.Equal(mockUsers[0].Email, result[0].Email);
        }

        [Fact]
        public void GetAllCustomers_Should_Throw_Exception_If_Exception_Occurs()
        {
            // Arrange
            _userRepository.Setup(repo => repo.GetAllCustomersFromDatabase()).Throws(new Exception("Simulated database error"));

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _userService.GetAllCustomser());

            Assert.Equal("Simulated database error", exception.Message);
        }
    }
}
