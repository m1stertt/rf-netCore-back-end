using System.IO;
using Moq;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;
using ScrumMasters.Webshop.Domain.Service;
using Xunit;

namespace ScrumMasters.Webshop.Domain.Test
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _mock;
        private readonly UserService _service;
        private readonly User _expected;

        public UserServiceTest()
        {
            _mock = new Mock<IUserRepository>();
            _service = new UserService(_mock.Object);
            _expected = new User
            {
                Id = 1,
                FirstName = "User1"
            };
        }

        [Fact]
        public void UsersService_IsIUserService()
        {
            Assert.True(_service is IUserService);
        }

        [Fact]
        public void UsersService_WithNullUserRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new UserService(null)
            );
        }

        [Fact]
        public void UsersService_WithNullUserRepository_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new UserService(null)
            );
            Assert.Equal("UserRepository Cannot Be Null", exception.Message);
        }

        [Fact]
        public void GetUser_WithId_ReturnsUser()
        {
            _mock.Setup(r => r.GetUserById(1))
                .Returns(_expected);
            var actual = _service.GetUserById(1);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void GetUser_WithIdZero_ReturnsNull()
        {
            _mock.Setup(r => r.GetUserById(0))
                .Returns((User)null);
            var actual = _service.GetUserById(0);
            Assert.Null(actual);
        }
        
        [Fact]
        public void DeleteUser_WithIdZero_ReturnsNull()
        {
            _mock.Setup(r => r.DeleteById(0))
                .Returns((User)null);
            var actual = _service.DeleteById(0);
            Assert.Null(actual);
        }
        
        [Fact]
        public void CreateUser_WithUserNull_ReturnsNull()
        {
            _mock.Setup(r => r.Create(_expected))
                .Returns((User)null);
            var actual = _service.Create(null);
            Assert.Null(actual);
        }
        
        [Fact]
        public void UpdateUser_WithUserNull_ReturnsNull()
        {
            _mock.Setup(r => r.Create(_expected))
                .Returns((User)null);
            var actual = _service.Create(null);
            Assert.Null(actual);
        }
        [Fact]
        public void DeleteUserById_ReturnsUser()
        {
            User _expected = new User();
            
            _mock.Setup(r => r.DeleteById(1))
                .Returns(_expected);
            var actual = _service.DeleteById(1);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void UpdateUserWithUser_ReturnsUser()
        {
            User _expected = new User();
            
            _mock.Setup(r => r.Update(_expected))
                .Returns(_expected);
            var actual = _service.Update(_expected);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void CreateUserWithUser_ReturnsUser()
        {
            User _expected = new User();
            
            _mock.Setup(r => r.Create(_expected))
                .Returns(_expected);
            var actual = _service.Create(_expected);
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void CheckUserWithEmail_ReturnsTrue()
        {
            var mock = "test@test.dk";
            
            _mock.Setup(r => r.CheckByEmail(mock))
                .Returns(true);
            var actual = _service.CheckUserByEmail(mock);
            Assert.True(actual);
        }
    }
}