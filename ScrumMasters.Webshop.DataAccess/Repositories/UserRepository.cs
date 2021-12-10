using System.IO;
using System.Linq;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly MainDbContext _context;

        public UserRepository(MainDbContext ctx)
        {
            _context = ctx ?? throw new InvalidDataException("User Repository Must have a DBContext");
        }
        public User Create(User user)
        {
            var ue = new UserEntity()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                StreetAndNumber = user.StreetAndNumber,
                PostalCode = user.PostalCode,
                City = user.City,
                PhoneNumber = user.PhoneNumber
            };
            var deletedEntity = _context.Users.Add(ue).Entity;
            _context.SaveChanges();
            return new User()
            {
                Id = deletedEntity.Id,
                FirstName = deletedEntity.FirstName,
                LastName = deletedEntity.LastName,
                Email = deletedEntity.Email,
                StreetAndNumber = deletedEntity.StreetAndNumber,
                PostalCode = deletedEntity.PostalCode,
                City = deletedEntity.City,
                PhoneNumber = deletedEntity.PhoneNumber
            };
        }

        public User FindById(int id)
        {
            return _context.Users.Select(ue => new User()
            {
                Id = ue.Id,
                FirstName = ue.FirstName,
                LastName = ue.LastName,
                Email = ue.Email,
                StreetAndNumber = ue.StreetAndNumber,
                PostalCode = ue.PostalCode,
                City = ue.City,
                PhoneNumber = ue.PhoneNumber
                
            }).FirstOrDefault(user => user.Id == id);
        }

        public User Update(User user)
        {
            var ue = _context.Update(new UserEntity()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                StreetAndNumber = user.StreetAndNumber,
                PostalCode = user.PostalCode,
                City = user.City,
                PhoneNumber = user.PhoneNumber
            }).Entity;
            _context.SaveChanges();
            return new User()
            {
                Id = ue.Id,
                FirstName = ue.FirstName,
                LastName = ue.LastName,
                Email = ue.Email,
                StreetAndNumber = ue.StreetAndNumber,
                PostalCode = ue.PostalCode,
                City = ue.City,
                PhoneNumber = ue.PhoneNumber
            };
        }

        public User DeleteById(int id)
        {
            var deletedEntity = _context.Users.Remove(new UserEntity() {Id = id}).Entity;
            _context.SaveChanges();
            return new User()
            {
                Id = deletedEntity.Id,
                FirstName = deletedEntity.FirstName,
                LastName = deletedEntity.LastName,
                Email = deletedEntity.Email,
                StreetAndNumber = deletedEntity.StreetAndNumber,
                PostalCode = deletedEntity.PostalCode,
                City = deletedEntity.City,
                PhoneNumber = deletedEntity.PhoneNumber
            };
        }
    }
}