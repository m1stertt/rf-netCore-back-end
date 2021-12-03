using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private readonly MainDbContext _context;

        public SizeRepository(MainDbContext ctx)
        {
            _context = ctx ?? throw new InvalidDataException("Size Repository Must have a DBContext");
        }

        public List<Size> FindAll()
        {
            return _context.Sizes.Select(pe => new Size
                {
                    Id = pe.Id,
                    Title=pe.Title,
                    Products = pe.Products.Select(px => new Product {Id = px.Id, ProductName = px.ProductName}).ToList()
                })
                .ToList();
        }

        public Size FindById(int id)
        {
            return _context.Sizes.Select(pe => new Size()
            {
                Id = pe.Id,
                Title=pe.Title,
                Products = pe.Products.Select(px => new Product {Id = px.Id, ProductName = px.ProductName}).ToList()
            }).FirstOrDefault(size => size.Id == id);
        }

        public Size Create(Size size)
        {
            var entity = new SizeEntity()
            {
                Title=size.Title,
                //Products = pe.Products.Select(px => new Product {Id = px.Id, ProductName = px.ProductName}).ToList()
            };
            var savedEntity = _context.Sizes.Add(entity).Entity;
            _context.SaveChanges();
            return new Size()
            {
                Id = size.Id,
                Title=size.Title,
            };
        }

        public Size Update(Size size)
        {
            var pe = _context.Update(new SizeEntity
            {
                Id = size.Id,
                Title = size.Title
            }).Entity;
            _context.SaveChanges();
            return new Size
            {
                Id = pe.Id,
                Title = pe.Title
            };
        }

        public Size DeleteById(int id)
        {
            var savedEntity = _context.Sizes.Remove(new SizeEntity() {Id = id}).Entity;
            _context.SaveChanges();
            return new Size()
            {
                Id = savedEntity.Id,
                Title = savedEntity.Title,
            };
        }
    }
}