using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Domain.IRepositories
{
    public interface ISizeRepository
    {
        List<Size> FindAll();
        Size FindById(int id);
        Size Create(Size size);
        Size Update(Size size);
        Size DeleteById(int id);
    }
}