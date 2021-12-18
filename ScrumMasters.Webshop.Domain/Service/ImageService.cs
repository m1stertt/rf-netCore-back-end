using System;
using System.Collections.Generic;
using System.IO;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.Domain.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository ?? throw new InvalidDataException("ImageRepository Cannot Be Null");
        }

        public Image GetById(int id)
        {
            return _imageRepository.GetById(id);
        }

        public Image Create(Image image)
        {
            return _imageRepository.Create(image);
        }

        public Image Update(Image image)
        {
            return _imageRepository.Update(image);
        }

        public Image DeleteById(int id)
        {
            return _imageRepository.DeleteById(id);
        }
    }
}