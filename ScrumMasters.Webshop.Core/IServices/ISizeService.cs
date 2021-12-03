using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Core.IServices
{
    public interface ISizeService
    {
        List<Size> GetSizes();
        Size GetSizeById(int id);
        Size Create(Size size);
        Size Update(Size size);
        Size DeleteById(int id);

    }
}