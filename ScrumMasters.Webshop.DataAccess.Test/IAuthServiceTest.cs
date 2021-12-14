using System.Collections.Generic;
using Moq;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Security;
using ScrumMasters.Webshop.Security.Model;
using Xunit;

namespace TeScrumMasters.Webshop.DataAccess.Test
{
    public class IAuthServiceTest
    {
        [Fact]
            public void IAuthService_IsAvailable()
            {
                var service = new Mock<IAuthService>().Object;
                Assert.NotNull(service);
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
            
        }
    }
