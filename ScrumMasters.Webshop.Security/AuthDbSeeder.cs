using ScrumMasters.Webshop.Security.Model;
using ScrumMasters.Webshop.Security.Services;

namespace ScrumMasters.Webshop.Security
{
    public class AuthDbSeeder : IAuthDbSeeder
    {
        private readonly AuthDbContext authDbContext;
        private readonly IAuthService _authService;

        public AuthDbSeeder(
            AuthDbContext ctx,
            IAuthService authService)
        {
            authDbContext = ctx;
            _authService = authService;
        }

        public void SeedDevelopment()
        {
            authDbContext.Database.EnsureDeleted();
            authDbContext.Database.EnsureCreated();
            AuthService.CreateHashAndSalt("123456", out var passwordHash, out var salt);

            authDbContext.LoginUsers.Add(new LoginUser
            {
                Email = "ljuul@ljuul.dk",
                HashedPassword = passwordHash,
                PasswordSalt = salt,
                DbUserId = 1,
            });
            AuthService.CreateHashAndSalt("123456", out passwordHash, out salt);
            authDbContext.LoginUsers.Add(new LoginUser
            {
                Email = "ljuul2@ljuul.dk",
                HashedPassword = passwordHash,
                PasswordSalt = salt,
                DbUserId = 2,
            });
            authDbContext.Permissions.AddRange(new Permission()
            {
                Name = "Admin"
            }, new Permission()
            {
                Name = "CanManageProducts"
            }, new Permission()
            {
                Name = "CanManageCategories"
            }, new Permission()
            {
                Name = "CanManageAccount"
            }, new Permission());
            authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 1, UserId = 1});
            authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 2, UserId = 1});
            authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 3, UserId = 2});
            authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 4, UserId = 2});
            authDbContext.SaveChanges();
        }

        public void SeedProduction()
        {
            authDbContext.Database.EnsureCreated();

        }
    }
}