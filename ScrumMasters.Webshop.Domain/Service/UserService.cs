using System.IO;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.Domain.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository ?? throw new InvalidDataException("UserRepository Cannot Be Null");
        }


        public bool CheckUserByEmail(string email)
        {
            return _UserRepository.CheckByEmail(email);
        }

        public User GetUserById(int id)
        {
            return _UserRepository.GetUserById(id);
        }

        public User Create(User User)
        {
            return _UserRepository.Create(User);
        }

        public User Update(User User)
        {
            return _UserRepository.Update(User);
        }

        public User DeleteById(int id)
        {
            return  _UserRepository.DeleteById(id);
        }
    }
}
