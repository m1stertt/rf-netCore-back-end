﻿using System;
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
            return id <= 0 ? null : _imageRepository.GetById(id);
        }

        public Image Create(Image image)
        {
            return _imageRepository.Create(image);
        }

        public Image Update(Image image)
        {
            return image == null ? null : _imageRepository.Update(image);
        }

        public Image DeleteById(int id)
        {
            return id <= 0 ? null : _imageRepository.DeleteById(id);
        }

        public List<Image> GetByProductId(int id)
        {
            return id <= 0 ? null : _imageRepository.GetByProductId(id);
        }
    }
}