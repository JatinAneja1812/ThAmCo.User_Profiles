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
    public class GetUserByUsernameAndEmailFromDatabaseTest
    {
        private readonly UserRepository _userRepository;
        private readonly Mock<ProfilesContext> _contextMock;
        private readonly Mock<ILogger<UserRepository>> _loggerMock;

        public GetUserByUsernameAndEmailFromDatabaseTest()
        {
            // Arrange
            _contextMock = new Mock<ProfilesContext>();
            _loggerMock = new Mock<ILogger<UserRepository>>();
            _userRepository = new UserRepository(_contextMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetUserByUsernameAndEmailFromDatabase_Should_Return_User_IfExists()
        {
            // Arrange
            var username = "JohnDoe";
            var email = "john.doe@example.com";
            var userData = new List<User>
            {
                new User
                {
                    UserId = "dssad321",
                    Username = username,
                    Email = email,
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
            var result = _userRepository.GetUserByUsernameAndEmailFromDatabase(username, email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("dssad321", result.UserId);
            Assert.Equal(username, result.Username);
            Assert.Equal(email, result.Email);
        }

        [Fact]
        public void GetUserByUsernameAndEmailFromDatabase_Should_Return_Null_If_User_NotFound()
        {
            // Arrange
            var nonExistentUsername = "NonExistentUser";
            var nonExistentEmail = "nonexistent@example.com";

            var mockUserDbSet = new Mock<DbSet<User>>();
            _contextMock.Setup(c => c.Users).Returns(mockUserDbSet.Object);

            // Act
            var result = _userRepository.GetUserByUsernameAndEmailFromDatabase(nonExistentUsername, nonExistentEmail);

            // Assert
            Assert.Null(result);
        }
    }
}
