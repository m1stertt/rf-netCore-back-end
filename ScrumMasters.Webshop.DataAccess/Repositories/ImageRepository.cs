using System;
using System.Collections.Generic;
using System.IO;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{
    public class ImageRepository : IImageRepository
    {
        
        //@todo
        private readonly MainDbContext _context;

        public ImageRepository(MainDbContext ctx)
        {
            _context = ctx ?? throw new InvalidDataException("Image Repository Must have a DBContext");
        }

        public Image GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Image Create(Image image)
        {
            var imageEntity = new ImageEntity()
            {
                Id = image.Id,
                Title = image.Title,
                Path = image.Path,
                Desc = image.Desc,
                Product = new ProductEntity{ Id = image.Product.Id}
            };
            var savedEntity = _context.Images.Add(imageEntity).Entity;
            _context.SaveChanges();
            return new Image()
            {
                Id = savedEntity.Id,
                Title = savedEntity.Title,
                Path = savedEntity.Path,
                Desc = savedEntity.Desc,
                Product = new Product{ Id=savedEntity.Product.Id }
            };
        }

        public Image Update(Image image)
        {
            if (image == null) return null;
            var savedEntity = _context.Update(new ImageEntity
            {
                Id = image.Id,
                Title = image.Title,
                Path = image.Path,
                Desc = image.Desc,
            }).Entity;
            _context.SaveChanges();
            return new Image
            {
                Id = savedEntity.Id,
                Title = savedEntity.Title,
                Path = savedEntity.Path,
                Desc = savedEntity.Desc,
                Product = new Product{ Id=savedEntity.Product.Id }
            };
        }

        public Image DeleteById(int id)
        {
            if (id == 0) return null;
            var savedEntity = _context.Images.Remove(new ImageEntity() {Id = id}).Entity;
            _context.SaveChanges();
            return new Image()
            {
                Id = savedEntity.Id,
                Title = savedEntity.Title,
                Path = savedEntity.Path,
                Desc = savedEntity.Desc,
                Product = new Product{ Id=savedEntity.Product.Id }
            };
        }
    }
}