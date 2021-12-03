using System.Collections.Generic;
using System.IO;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.Domain.Service
{
    public class ColorService: IColorService
    {
        private readonly IColorRepository _colorRepository;

        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository ?? throw new InvalidDataException("ColorRepository Cannot Be Null");
        }
        public List<Color> GetColors()
        {
            return _colorRepository.FindAll();
        }

        public Color Create(Color color)
        {
            return _colorRepository.Create(color);
        }

        public Color GetColorById(int id)
        {
            return _colorRepository.FindById(id);
        }

        public Color Update(Color color)
        {
            return _colorRepository.Update(color);
        }

        public Color DeleteById(int id)
        {
            return  _colorRepository.DeleteById(id);
        }
    }
}