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
    public class GetAllCustomersFromDatabaseTest
    {
        private readonly UserRepository _userRepository;
        private readonly Mock<ProfilesContext> _contextMock;
        private readonly Mock<ILogger<UserRepository>> _loggerMock;

        public GetAllCustomersFromDatabaseTest()
        {
            // Arrange
            _contextMock = new Mock<ProfilesContext>();
            _loggerMock = new Mock<ILogger<UserRepository>>();

            _userRepository = new UserRepository(_contextMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetAllCustomersFromDatabase_Should_Return_Customers_List()
        {
            //Arrange
            var data = new List<User>
            {
                new User
                {
                    UserId = "dssad321",
                    Username = "JohnDoe",
                    Email = "john.doe@example.com",
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
                },
                new User
                {
                    UserId = "243ewfe",
                    Username = "JaneSmith",
                    Email = "jane.smith@example.com",
                    FirstName = "Jane",
                    LastName = "Smith",
                    PhoneNumber = "987-654-3210",
                    AvailableFunds = 150.00,
                    UserAddedOnDate = DateTime.Now,
                    UserType = UserTypeEnum.Customer,
                    LocationNumber = "456",
                    Street = "High Street",
                    City = "Townsville",
                    State = "TS",
                    PostalCode = "54321"
                }
            }.AsQueryable();


            var mockUserDbSet = new Mock<DbSet<User>>();
            mockUserDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockUserDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockUserDbSet.As<IQueryable<User>> ().Setup(m => m.ElementType).Returns(data.ElementType);
            mockUserDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _contextMock.Setup(c => c.Users).Returns(mockUserDbSet.Object);

            //Act
            var result = _userRepository.GetAllCustomersFromDatabase();

            IEnumerable<User> actual = result.ToList();
            //Assert
            Assert.True(result != null);
            Assert.Equal(2, actual.Count());
            Assert.True(actual.ToList()[0].UserId == "dssad321");
            Assert.True(actual.ToList()[1].UserId == "243ewfe");
        }

        [Fact]
        public void Should_Fail_If_Context_Returns_Null()
        {
            // Arrange
            Mock<DbSet<User>> mockUserDbSet = new Mock<DbSet<User>>();
            var data = new List<User>
            {
                new User
                {
                    UserId = "dssad321",
                    Username = "JohnDoe",
                    Email = "john.doe@example.com",
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
                },
                new User
                {
                    UserId = "243ewfe",
                    Username = "JaneSmith",
                    Email = "jane.smith@example.com",
                    FirstName = "Jane",
                    LastName = "Smith",
                    PhoneNumber = "987-654-3210",
                    AvailableFunds = 150.00,
                    UserAddedOnDate = DateTime.Now,
                    UserType = UserTypeEnum.Customer,
                    LocationNumber = "456",
                    Street = "High Street",
                    City = "Townsville",
                    State = "TS",
                    PostalCode = "54321"
                }
            }.AsQueryable();

            _contextMock.SetupGet(c => c.Users).Returns((DbSet<User>)null); // Simulate _context.Users returning null

            var result = _userRepository.GetAllCustomersFromDatabase();

            // Assert
            Assert.Null(result);
        }
    }
}
