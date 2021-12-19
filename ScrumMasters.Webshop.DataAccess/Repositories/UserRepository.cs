using System.IO;
using System.Linq;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MainDbContext _context;

        public UserRepository(MainDbContext ctx)
        {
            _context = ctx ?? throw new InvalidDataException("User Repository Must have a DBContext");
        }

        public User Create(User user)
        {
            if (user == null) return null;
            var ue = new UserEntity()
            {
                Id = user.Id,
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

        public bool CheckByEmail(string email)
        {
            if (email.Equals("")) return false;
            if (_context.Users.Any(user => email.Equals(user.Email)))
            {
                return true;
            }

            return false;
        }

        public User GetUserById(int id)
        {
            if (id == 0) return null;
            return _context.Users.Select(pe => new User()
            {
                Id = pe.Id,
                FirstName = pe.FirstName,
                LastName = pe.LastName,
                Email = pe.Email,
                StreetAndNumber = pe.StreetAndNumber,
                PostalCode = pe.PostalCode,
                City = pe.City,
                PhoneNumber = pe.PhoneNumber
            }).FirstOrDefault(product => product.Id == id);
        }

        public User Update(User user)
        {
            if (user == null) return null;
            var ue = _context.Update(new UserEntity()
            {
                Id = user.Id,
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
            if (id == 0) return null;
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