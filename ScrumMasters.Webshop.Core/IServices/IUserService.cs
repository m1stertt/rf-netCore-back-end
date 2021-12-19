using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Core.IServices
{
    public interface IUserService
    {
        bool CheckUserByEmail(string id);
        User GetUserById(int id);
        User Create(User user);
        User Update(User user);
        User DeleteById(int id);
    }
}