using AutoMapper;
using Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using ThAmCo.User_Profiles.Automapper;
using ThAmCo.User_Profiles.Models;
using ThAmCo.User_Profiles.Repositories.Repository.Interfaces;
using ThAmCo.User_Profiles.Services.Service.Classes;
using Xunit;

namespace UserProfilesService.Tests
{
    public class DeleteUserTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ILogger<UserService>> _logger;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public DeleteUserTest()
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
        public void DeleteUser_Should_Return_True_If_User_Is_Deleted_Successfully()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var existingUser = new User { UserId = Guid.Parse(userId) };

            _userRepository.Setup(repo => repo.GetUserByIdFromDatabase(userId)).Returns(existingUser);
            _userRepository.Setup(repo => repo.DeleteUserFromDatabase(existingUser)).Returns(1); // Assuming 1 means successful deletion

            // Act
            var result = _userService.DeleteUser(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteUser_Should_Return_False_If_User_Failed_To_Get_Removed()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _userRepository.Setup(repo => repo.GetUserByIdFromDatabase(userId)).Returns(new User { UserId = Guid.Parse(userId) });
            _userRepository.Setup(repo => repo.DeleteUserFromDatabase(It.IsAny<User>())).Throws(new Exception("Some unexpected exception"));

            // Act
            var result = _userService.DeleteUser(userId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DeleteUser_Should_Throw_DataNotFoundException_If_User_Is_Not_Found()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _userRepository.Setup(repo => repo.GetUserByIdFromDatabase(userId)).Returns((User)null);

            // Act & Assert
            Assert.Throws<DataNotFoundException>(() => _userService.DeleteUser(userId));
        }
    }
}
