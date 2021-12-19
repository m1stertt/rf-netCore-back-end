using System.Collections.Generic;
using System.IO;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.Domain.Service
{
    public class SizeService: ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository ?? throw new InvalidDataException("SizeRepository Cannot Be Null");
        }
        public List<Size> GetSizes()
        {
            return _sizeRepository.FindAll();
        }

        public Size Create(Size size)
        {
            return _sizeRepository.Create(size);
        }

        public Size GetSizeById(int id)
        {
            return _sizeRepository.FindById(id);
        }

        public Size Update(Size size)
        {
            return _sizeRepository.Update(size);
        }

        public Size DeleteById(int id)
        {
            return  _sizeRepository.DeleteById(id);
        }
    }
}