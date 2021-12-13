using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Core.IServices
{
    public interface IImageService
    {
        Image GetById(int id);
        Image Create(Image image);
        Image Update(Image image);
        Image DeleteById(int id);
    }
}