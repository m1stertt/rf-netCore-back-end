using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Core.IServices
{
    public interface IColorService
    {
        List<Color> GetColors();
        Color GetColorById(int id);
        Color Create(Color color);
        Color Update(Color color);
        Color DeleteById(int id);
    }
}