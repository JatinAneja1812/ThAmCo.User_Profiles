using AutoMapper;
using Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using ThAmCo.User_Profiles.Automapper;
using ThAmCo.User_Profiles.DTOs;
using ThAmCo.User_Profiles.Models;
using ThAmCo.User_Profiles.Repositories.Repository.Interfaces;
using ThAmCo.User_Profiles.Services.Service.Classes;
using ThAmCo.User_Profiles.Utility;
using Xunit;

namespace UserProfilesService.Tests
{
    public class UpdateCustomerFundsTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ILogger<UserService>> _logger;
        private readonly Mock<IGuidUtility> _guidUtility;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public UpdateCustomerFundsTest()
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
        public void UpdateCustomerFunds_Should_Update_User_Funds_And_Return_True()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var updatedCustomerFunds = new CustomerFundsDTO
            {
                UserId = userId,
                Amount = 500.00 // Set the updated amount
            };

            var existingUser = new User
            {
                UserId = userId,
                AvailableFunds = 100.00 // Set the initial available funds
            };

            _userRepository.Setup(repo => repo.GetUserByIdFromDatabase(userId)).Returns(existingUser);
            _userRepository.Setup(repo => repo.UpdateUserToDatabase(It.IsAny<User>())).Returns(1); // Assuming 1 means successful update

            // Act
            var result = _userService.UpdateCustomerFunds(updatedCustomerFunds);

            // Assert
            Assert.True(result);
            _userRepository.Verify(repo => repo.UpdateUserToDatabase(It.IsAny<User>()), Times.Once);
            Assert.Equal(updatedCustomerFunds.Amount, existingUser.AvailableFunds); // Verify that funds are updated
        }

        [Fact]
        public void UpdateCustomerFunds_Should_Return_False_If_Update_Fails()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var updatedCustomerFunds = new CustomerFundsDTO
            {
                UserId = userId,
                Amount = 500.00 // Set the updated amount
            };

            var existingUser = new User
            {
                UserId = userId,
                AvailableFunds = 100.00 // Set the initial available funds
            };

            _userRepository.Setup(repo => repo.GetUserByIdFromDatabase(userId)).Returns(existingUser);
            _userRepository.Setup(repo => repo.UpdateUserToDatabase(It.IsAny<User>())).Returns(0); // Assuming 0 means update failure

            // Act
            var result = _userService.UpdateCustomerFunds(updatedCustomerFunds);

            // Assert
            Assert.False(result);
            _userRepository.Verify(repo => repo.UpdateUserToDatabase(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void UpdateCustomerFunds_Should_Throw_DataNotFoundException_If_User_Not_Found()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _userRepository.Setup(repo => repo.GetUserByIdFromDatabase(userId)).Returns((User)null);

            // Act & Assert
            Assert.Throws<DataNotFoundException>(() => _userService.UpdateCustomerFunds(new CustomerFundsDTO { UserId = userId, Amount = 500.00 }));
            _userRepository.Verify(repo => repo.UpdateUserToDatabase(It.IsAny<User>()), Times.Never); // Ensure that UpdateUserToDatabase is not called
        }

        [Fact]
        public void UpdateCustomerFunds_Should_Return_False_If_Exception_Occurs()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var updatedCustomerFunds = new CustomerFundsDTO
            {
                UserId = userId,
                Amount = 500.00 // Set the updated amount
            };

            _userRepository.Setup(repo => repo.GetUserByIdFromDatabase(userId)).Throws(new Exception("Simulated exception"));

            // Act
            var result = _userService.UpdateCustomerFunds(updatedCustomerFunds);

            // Assert
            Assert.False(result);
            _userRepository.Verify(repo => repo.UpdateUserToDatabase(It.IsAny<User>()), Times.Never); // Ensure that UpdateUserToDatabase is not called
        }
    }
}
