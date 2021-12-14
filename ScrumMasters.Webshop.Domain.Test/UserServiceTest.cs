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
    }
}