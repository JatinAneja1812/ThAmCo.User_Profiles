using AutoMapper;
using Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using ThAmCo.User_Profiles.Automapper;
using ThAmCo.User_Profiles.DTOs;
using ThAmCo.User_Profiles.Models;
using ThAmCo.User_Profiles.Repositories.Repository.Interfaces;
using ThAmCo.User_Profiles.Services.Service.Classes;
using Xunit;

namespace UserProfilesService.Tests
{
    public class UpdateUserTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ILogger<UserService>> _logger;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public UpdateUserTest()
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
        public void UpdateUser_Should_Update_User_Properties_And_Return_True_If_Update_Successful()
        {
            // Arrange
            var userDataToUpdate = new UserProfilesDTO
            {
                UserId = Guid.NewGuid(),
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                PhoneNumber = "9876543210",
                LocationNumber = "456",
                Street = "Updated Street",
                City = "Updated City",
                State = "Updated State",
                PostalCode = "UPD12345",
            };

            var existingUser = new User
            {
                UserId = userDataToUpdate.UserId,
                FirstName = "OldFirstName",
                LastName = "OldLastName",
                PhoneNumber = "1234567890",
                LocationNumber = "123",
                Street = "Old Street",
                City = "Old City",
                State = "Old State",
                PostalCode = "OLD12345",
            };

            _userRepository.Setup(repo => repo.GetUserByIdFromDatabase(userDataToUpdate.UserId.ToString())).Returns(existingUser);
            _userRepository.Setup(repo => repo.UpdateUserToDatabase(existingUser)).Returns(1);

            // Act
            var result = _userService.UpdateUser(userDataToUpdate);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateUser_Should_Return_False_If_Update_Fails()
        {
            // Arrange
            var userDataToUpdate = new UserProfilesDTO
            {
                UserId = Guid.NewGuid(),
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                PhoneNumber = "9876543210",
                LocationNumber = "456",
                Street = "Updated Street",
                City = "Updated City",
                State = "Updated State",
                PostalCode = "UPD12345",
            };

            var existingUser = new User
            {
                UserId = userDataToUpdate.UserId,
                FirstName = "OldFirstName",
                LastName = "OldLastName",
                PhoneNumber = "1234567890",
                LocationNumber = "123",
                Street = "Old Street",
                City = "Old City",
                State = "Old State",
                PostalCode = "OLD12345",
            };

            _userRepository.Setup(repo => repo.GetUserByIdFromDatabase(userDataToUpdate.UserId.ToString())).Returns(existingUser);
            _userRepository.Setup(repo => repo.UpdateUserToDatabase(existingUser)).Returns(0);

            // Act
            var result = _userService.UpdateUser(userDataToUpdate);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateUser_Should_Return_False_If_Exception_Occurs()
        {
            // Arrange
            var userDataToUpdate = new UserProfilesDTO
            {
                UserId = Guid.NewGuid(),
                FirstName = "OldFirstName",
                LastName = "OldLastName",
                PhoneNumber = "1234567890",
                LocationNumber = "123",
                Street = "Old Street",
                City = "Old City",
                State = "Old State",
                PostalCode = "OLD12345",
                /* Set other user properties to be updated */
            };

            _userRepository.Setup(repo => repo.GetUserByIdFromDatabase(userDataToUpdate.UserId.ToString())).Throws(new Exception("Simulated error"));

            // Act
            var result = _userService.UpdateUser(userDataToUpdate);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateUser_Should_Throw_DataNotFoundException_If_User_Not_Found()
        {
            // Arrange
            var userDataToUpdate = new UserProfilesDTO
            {
                UserId = Guid.NewGuid(),
                FirstName = "OldFirstName",
                LastName = "OldLastName",
                PhoneNumber = "1234567890",
                LocationNumber = "123",
                Street = "Old Street",
                City = "Old City",
                State = "Old State",
                PostalCode = "OLD12345",
            };

            _userRepository.Setup(repo => repo.GetUserByIdFromDatabase(userDataToUpdate.UserId.ToString())).Returns((User)null);

            // Act & Assert
            var exception = Assert.Throws<DataNotFoundException>(() => _userService.UpdateUser(userDataToUpdate));
            
        }
    }
}
