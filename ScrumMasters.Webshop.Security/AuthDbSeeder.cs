using ScrumMasters.Webshop.Security.Model;
using ScrumMasters.Webshop.Security.Services;

namespace ScrumMasters.Webshop.Security
{
    public class AuthDbSeeder : IAuthDbSeeder
    {
        private readonly AuthDbContext authDbContext;

        public AuthDbSeeder(
            AuthDbContext ctx)
        {
            authDbContext = ctx;
        }

        public void SeedDevelopment()
        {
            authDbContext.Database.EnsureDeleted();
            authDbContext.Database.EnsureCreated();
            AuthService.CreateHashAndSalt("123456", out var passwordHash, out var salt);

            authDbContext.LoginUsers.Add(new LoginUser
            {
                Email = "admin@admin.dk",
                HashedPassword = passwordHash,
                PasswordSalt = salt,
                DbUserId = 1,
            });
            AuthService.CreateHashAndSalt("123456", out passwordHash, out salt);
            authDbContext.LoginUsers.Add(new LoginUser
            {
                Email = "user@user.dk",
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
            }, new Permission
            {
                Name = "CanManageColors"
            },new Permission
            {
                Name = "CanManageSizes"
            });
            authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 1, UserId = 1});
            //authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 2, UserId = 1});
            //authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 3, UserId = 1});
            //authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 4, UserId = 1});
            //authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 5, UserId = 1});
            //authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 4, UserId = 2});
            authDbContext.SaveChanges();
        }

        public void SeedProduction()
        {
            // For now. Should be fixed for production ready code.
            authDbContext.Database.EnsureDeleted();
            
            authDbContext.Database.EnsureCreated();
            AuthService.CreateHashAndSalt("123456", out var passwordHash, out var salt);

            authDbContext.LoginUsers.Add(new LoginUser
            {
                Email = "admin@admin.dk",
                HashedPassword = passwordHash,
                PasswordSalt = salt,
                DbUserId = 1,
            });
            AuthService.CreateHashAndSalt("123456", out passwordHash, out salt);
            authDbContext.LoginUsers.Add(new LoginUser
            {
                Email = "user@user.dk",
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
            }, new Permission
            {
                Name = "CanManageColors"
            },new Permission
            {
                Name = "CanManageSizes"
            });
            authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 1, UserId = 1});
            //authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 4, UserId = 2});
            authDbContext.SaveChanges();
        }
    }
}