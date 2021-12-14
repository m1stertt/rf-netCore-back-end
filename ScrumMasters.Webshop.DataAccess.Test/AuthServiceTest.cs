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
        
        private readonly Mock<IAuthRepository> _mock;

        
        public AuthServiceTest()
        {
            _mock = new Mock<IAuthRepository>();
            _configuration = new ConfigurationManager();
            _service = new AuthService(_configuration, _mock.Object);
            
            
            // _service = new UserService(_mock.Object);
            // _expected = new User
            // {
            //     Id = 1,
            //     FirstName = "User1"
            // };
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