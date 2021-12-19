using System;
using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Domain.IRepositories
{
    public interface IImageRepository
    {
        Image GetById(int id);
        Image Create(Image image);
        Image Update(Image image);
        Image DeleteById(int id);
        List<Image> GetByProductId(int id);
    }
}