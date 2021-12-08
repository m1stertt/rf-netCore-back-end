using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Domain.IRepositories
{
    public interface IImageRepository
    {
        List<Image> GetAll();
        Image GetById(int id);
        Image Create(Image image);
        Image Update(Image image);
        Image DeleteById(int id);
    }
}