using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Moq;
using ScrumMasters.Webshop.Security;
using ScrumMasters.Webshop.Security.Model;
using ScrumMasters.Webshop.Security.Services;
using Xunit;

namespace TeScrumMasters.Webshop.DataAccess.Test
{
    public class AuthServiceTest
    {
        private readonly IAuthService _service;
        private readonly IConfiguration _configuration;
        private readonly Mock<IAuthRepository> _mock;

        
        public AuthServiceTest()
        {
            _mock = new Mock<IAuthRepository>();
            _configuration = new ConfigurationManager();
            _service = new AuthService(_configuration, _mock.Object);
        }
        
        [Fact]
        public void AuthService_IsAuthService()
        {
            Assert.True(_service is IAuthService);
        }

        [Fact]
        public void GetPermissions_WithUserId_ReturnsPermissions()
        {
            var fakePermissionsList = new List<Permission>();
            _mock.Setup(s => s.GetPermissions(1))
                .Returns(fakePermissionsList);
            var service = _mock.Object;
            Assert.Equal(fakePermissionsList, service.GetPermissions(1));
        }

        [Fact]
        public void UserRegistration_WithLoginUser_UserIsNotNull()
        {

            var fakeUserDetails = new UserDetails
            {
                Email = "test@test.dk",
                Password = "password"
            };
            var fakeUser = _service.RegisterUser(fakeUserDetails);
            Assert.NotNull(fakeUser);

        }
    }
}