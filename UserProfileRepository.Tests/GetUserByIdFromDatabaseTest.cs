using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.User_Profiles.DatabaseContext;
using ThAmCo.User_Profiles.Models;
using ThAmCo.User_Profiles.Repositories.Repository.Classes;
using Xunit;

namespace UserProfileRepository.Tests
{
    public class GetUserByIdFromDatabaseTest
    {
        private readonly UserRepository _userRepository;
        private readonly Mock<ProfilesContext> _contextMock;
        private readonly Mock<ILogger<UserRepository>> _loggerMock;

        public GetUserByIdFromDatabaseTest()
        {
            // Arrange
            _contextMock = new Mock<ProfilesContext>();
            _loggerMock = new Mock<ILogger<UserRepository>>();
            _userRepository = new UserRepository(_contextMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetUserByIdFromDatabase_Should_Return_User_IfExists()
        {
            // Arrange
            var userId = "dssad321";
            var userData = new List<User>
        {
            new User
            {
                UserId = userId,
                Username = "JohnDoe",
                Email = "john.doe@example.com",
                // Other user properties
            },
            new User
            {
                UserId = "243ewfe",
                Username = "JaneSmith",
                Email = "jane.smith@example.com",
                // Other user properties
            }
        }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<User>>();
            mockUserDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            mockUserDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            mockUserDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            mockUserDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator());

            _contextMock.Setup(c => c.Users).Returns(mockUserDbSet.Object);

            // Act
            var result = _userRepository.GetUserByIdFromDatabase(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public void GetUserByIdFromDatabase_Should_Return_Null_If_User_NotFound()
        {
            // Arrange
            var nonExistentUserId = "NonExistentUser";
            var mockUserDbSet = new Mock<DbSet<User>>();
            _contextMock.Setup(c => c.Users).Returns(mockUserDbSet.Object);

            // Act
            var result = _userRepository.GetUserByIdFromDatabase(nonExistentUserId);

            // Assert
            Assert.Null(result);
        }
    }
}
