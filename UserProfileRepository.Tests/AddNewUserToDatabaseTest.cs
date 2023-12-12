using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ThAmCo.User_Profiles.DatabaseContext;
using ThAmCo.User_Profiles.Enums;
using ThAmCo.User_Profiles.Models;
using ThAmCo.User_Profiles.Repositories.Repository.Classes;
using Xunit;

namespace UserProfileRepository.Tests
{
    public class AddNewUserToDatabaseTest
    {
        private readonly UserRepository _userRepository;
        private readonly Mock<ProfilesContext> _contextMock;
        private readonly Mock<ILogger<UserRepository>> _loggerMock;

        public AddNewUserToDatabaseTest()
        {
            // Arrange
            _contextMock = new Mock<ProfilesContext>();
            _loggerMock = new Mock<ILogger<UserRepository>>();

            _userRepository = new UserRepository(_contextMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void AddNewUserToDatabase_Should_Add_User_To_Database_If_User_Does_Not_Exists_In_Database()
        {
            //Arrange
            Mock<DbSet<User>> mockUserDbSet = new Mock<DbSet<User>>();
            User user = new User
            {
                UserId = "123456",
                Username = "TestUser",
                Email = "testuser@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123-456-7890",
                AvailableFunds = 100.00,
                UserAddedOnDate = DateTime.Now,
                UserType = UserTypeEnum.Customer,
                LocationNumber = "123",
                Street = "Main Street",
                City = "Cityville",
                State = "ST",
                PostalCode = "12345"
            };

            var data = new List<User>
            {
                new User
                {
                    UserId = "123456dwsd",
                    Username = "TestUser2",
                    Email = "testuser22@example.com",
                    FirstName = "John",
                    LastName = "Smith",
                    PhoneNumber = "123-456-7890432",
                    AvailableFunds = 100.00,
                    UserAddedOnDate = DateTime.Now,
                    UserType = UserTypeEnum.Customer,
                    LocationNumber = "1232",
                    Street = "High Street",
                    City = "Cityville",
                    State = "ST",
                    PostalCode = "134532"
                }
             }.AsQueryable();

            mockUserDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockUserDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockUserDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockUserDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _contextMock.SetupGet(c => c.Users).Returns(mockUserDbSet.Object);

            //Act
            var res = _userRepository.AddNewUserToDatabase(user);

            //Assert
            mockUserDbSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Should_Fail_If_Context_Returns_Null()
        {
            // Arrange
            Mock<DbSet<User>> mockUserDbSet = new Mock<DbSet<User>>();
            User user = new User
            {
                UserId = "123456dwsd",
                Username = "TestUser2",
                Email = "testuser22@example.com",
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "123-456-7890432",
                AvailableFunds = 100.00,
                UserAddedOnDate = DateTime.Now,
                UserType = UserTypeEnum.Customer,
                LocationNumber = "1232",
                Street = "High Street",
                City = "Cityville",
                State = "ST",
                PostalCode = "134532"
            };

            _contextMock.SetupGet(c => c.Users).Returns((DbSet<User>)null); // Simulate _context.Users returning null

            var expected = _userRepository.AddNewUserToDatabase(user);

            // Act and Assert
            Assert.Equal(expected, -1);
            mockUserDbSet.Verify(m => m.Add(It.IsAny<User>()), Times.Never);
            _contextMock.Verify(m => m.SaveChanges(), Times.Never);
        }
    }
}
