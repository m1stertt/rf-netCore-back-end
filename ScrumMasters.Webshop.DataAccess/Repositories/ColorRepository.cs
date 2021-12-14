using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{
    public class ColorRepository: IColorRepository
    {
        private readonly MainDbContext _context;

        public ColorRepository(MainDbContext ctx)
        {
            _context = ctx ?? throw new InvalidDataException("Color Repository Must have a DBContext");
        }

        public List<Color> FindAll()
        {
            return _context.Colors.Select(pe => new Color
                {
                    Id = pe.Id,
                    Title=pe.Title,
                    Products = pe.Products.Select(px => new Product {Id = px.Id, ProductName = px.ProductName}).ToList(),
                    ColorString = pe.ColorString
                })
                .ToList();
        }

        public Color FindById(int id)
        {
            return _context.Colors.Select(pe => new Color()
            {
                Id = pe.Id,
                Title=pe.Title,
                Products = pe.Products.Select(px => new Product {Id = px.Id, ProductName = px.ProductName}).ToList(),
                ColorString = pe.ColorString
            }).FirstOrDefault(color => color.Id == id);
        }

        public Color Create(Color color)
        {
            var entity = new ColorEntity()
            {
                Title=color.Title,
                //Products = pe.Products.Select(px => new Product {Id = px.Id, ProductName = px.ProductName}).ToList()
            };
            var savedEntity = _context.Colors.Add(entity).Entity;
            _context.SaveChanges();
            return new Color()
            {
                Id = color.Id,
                Title=color.Title,
            };
        }

        public Color Update(Color color)
        {
            var pe = _context.Update(new ColorEntity
            {
                Id = color.Id,
                Title = color.Title,
                ColorString = color.ColorString
                
            }).Entity;
            _context.SaveChanges();
            return new Color
            {
                Id = pe.Id,
                Title = pe.Title,
                ColorString = pe.ColorString
            };
        }

        public Color DeleteById(int id)
        {
            var savedEntity = _context.Colors.Remove(new ColorEntity() {Id = id}).Entity;
            _context.SaveChanges();
            return new Color()
            {
                Id = savedEntity.Id,
                Title = savedEntity.Title,
            };
        }
    }
}