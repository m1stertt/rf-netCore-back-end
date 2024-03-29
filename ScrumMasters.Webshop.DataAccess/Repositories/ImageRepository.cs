﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{
    public class ImageRepository : IImageRepository
    {
        
        private readonly MainDbContext _context;

        public ImageRepository(MainDbContext ctx)
        {
            _context = ctx ?? throw new InvalidDataException("Image Repository Must have a DBContext");
        }

        public Image GetById(int id)
        {
            return _context.Images.Select(pe => new Image()
            {
                Id = pe.Id,
                Path = pe.Path,
                Title = pe.Title
            }).FirstOrDefault(image => image.Id == id);
        }

        public List<Image> GetByProductId(int id)
        {
            return _context.Images.Where(image => image.Product.Id == id).Select(pe => new Image()
            {
                Id = pe.Id,
                Path = pe.Path,
                Title = pe.Title
            }).ToList();
        }

        public Image Create(Image image)
        {
            var imageEntity = new ImageEntity()
            {
                Title = image.Title,
                Path = image.Path,
                Product = _context.Products.FirstOrDefault(r => r.Id == image.Product.Id)
            };
            var savedEntity = _context.Images.Add(imageEntity).Entity;
            _context.SaveChanges();
            return new Image()
            {
                Id = savedEntity.Id,
                Title = savedEntity.Title,
                Path = savedEntity.Path,
                Product = new Product{ Id=savedEntity.Product.Id }
            };
        }

        public Image Update(Image image)
        {
            var savedEntity = _context.Update(new ImageEntity
            {
                Id = image.Id,
                Title = image.Title,
                Path = image.Path,
            }).Entity;
            _context.SaveChanges();
            return new Image
            {
                Id = savedEntity.Id,
                Title = savedEntity.Title,
                Path = savedEntity.Path,
            };
        }

        public Image DeleteById(int id)
        {
            if (id <= 0) return null;
            var savedEntity = _context.Images.Remove(new ImageEntity() {Id = id}).Entity;
            _context.SaveChanges();
            return new Image()
            {
                Id = id,
            };
        }
    }
}