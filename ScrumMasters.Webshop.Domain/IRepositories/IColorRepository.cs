using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Domain.IRepositories
{
    public interface IColorRepository
    {
        List<Color> FindAll();
        List<Color> GetByProductId(int id);
        Color FindById(int id);
        Color Create(Color color);
        Color Update(Color color);
        Color DeleteById(int id);
    }
}