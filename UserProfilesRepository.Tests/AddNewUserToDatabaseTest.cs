using Microsoft.Extensions.Logging;
using Moq;
using ThAmCo.User_Profiles.DatabaseContext;
using ThAmCo.User_Profiles.Models;
using ThAmCo.User_Profiles.Repositories.Repository.Classes;
using Xunit;

namespace UserProfilesRepository.Tests
{
    public class AddNewUserToDatabaseTest
    {
        private readonly Mock<ProfilesContext> _context;
        private readonly Mock<ILogger<UserRepository>> _logger;
        private readonly UserRepository _userRepository;
        public AddNewUserToDatabaseTest()
        {
            _context = new Mock<ProfilesContext>();
            _logger = new Mock<ILogger<UserRepository>>();
            _userRepository = new UserRepository(_context.Object, _logger.Object);
        }

        [Fact]
        public void AddNewUserToDatabase_SuccessfulAddition_ReturnsOne()
        {
            // Arrange
            var userToAdd = new User
            {
                // Set properties for a valid user
                Username = "testUser",
                Email = "test@example.com"
                // Set other properties as needed
            };

            // Act
            var result = _userRepository.AddNewUserToDatabase(userToAdd);

            // Assert
            Assert.Equal(1, result);
            _context.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void AddNewUserToDatabase_FailedAddition_ReturnsMinusOne()
        {
            // Arrange
            var userToAdd = new User
            {
                // Set properties for an invalid user (e.g., missing required fields)
            };

            // Act
            var result = _userRepository.AddNewUserToDatabase(userToAdd);

            // Assert
            Assert.Equal(-1, result);
            _context.Verify(x => x.SaveChanges(), Times.Never);
            _logger.Verify(
                x => x.LogError(
                    It.IsAny<EventId>(),
                    It.IsAny<Exception>(),
                    It.IsAny<string>(),
                    It.IsAny<object[]>()),
                Times.Once);
        }
    }
}
