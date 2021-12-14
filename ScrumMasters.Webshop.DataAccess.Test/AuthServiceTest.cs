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
        private readonly AuthService _service;
        private readonly IConfiguration _configuration;
        private readonly AuthDbContext _ctx;
        
        public AuthServiceTest()
        {
            _service = new AuthService(_configuration, _ctx);
        }
        
        [Fact]
        public void AuthService_IsAuthService()
        {
            Assert.True(_service is IAuthService);
        }

        [Fact]
        public void GetPermissions_WithUserId_ReturnsPermissions()
        {
            var mock = new Mock<IAuthService>();
            var fakePermissionsList = new List<Permission>();
            mock.Setup(s => s.GetPermissions(1))
                .Returns(fakePermissionsList);
            var service = mock.Object;
            Assert.Equal(fakePermissionsList, service.GetPermissions(1));
        }
        [Fact]
        public void GetToken_WithLoginUser_ReturnsToken()
        {
            var mock = new Mock<IAuthService>();
            var token = "testToken";
            var fakeUser = new LoginUser();
            mock.Setup(s => s.GenerateJwtToken(fakeUser))
                .Returns(token);
            var service = mock.Object;
            Assert.Equal(token, service.GenerateJwtToken(fakeUser));
        }
        
        [Fact]
        public void UserRegistration_WithLoginUser_CallsOnce()
        {
            var mock = new Mock<IAuthService>().Object;
            var fakeUserDetails = new UserDetails
            {
                Email = "test@test.dk",
                Password = "password"
            };
            mock.RegisterUser(fakeUserDetails);
            _service.Verify(r => r.RegisterUser(fakeUserDetails), Times.Once);
        }
    }
}