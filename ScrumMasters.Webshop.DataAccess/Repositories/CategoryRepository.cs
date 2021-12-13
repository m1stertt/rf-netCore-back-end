using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MainDbContext context;
        
        public CategoryRepository(MainDbContext ctx)
        {
            context = ctx ?? throw new InvalidDataException("Category Repository Must have a DBContext");
        }
        public List<Category> FindAll()
        {
            return context.Categories
                .Select(pe => new Category
                {
                    Id = pe.Id,
                    Name = pe.Name,
                    Products = pe.Products.Select(px=>new Product{Id = px.Id,ProductName = px.ProductName}).ToList()
                })
                .ToList();
        }

        public Category GetById(int id)
        {
            return context.Categories
                .Select(pe => new Category
                {
                    Id = pe.Id,
                    Name = pe.Name,
                    Products = pe.Products.Select(px=>new Product{Id = px.Id,ProductName = px.ProductName}).ToList()
                }).FirstOrDefault(category => category.Id == id);
        }
        
        public Category DeleteById(int id)
        {
            var savedEntity = context.Categories.Remove(new CategoryEntity() {Id = id}).Entity;
            context.SaveChanges();
            return new Category()
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name,

            };
        }

        public Category Update(Category category)
        {
            var pe = context.Update(new CategoryEntity
            {
                Id = category.Id,
                Name = category.Name
            }).Entity;
            context.SaveChanges();
            return new Category
            {
                Id = pe.Id,
                Name = pe.Name
            };
        }

        public Category Create(Category category)
        {
            var entity = new CategoryEntity()
            {
                Name = category.Name,
            };
            var savedEntity = context.Add(entity).Entity;
            context.SaveChanges();
            return new Category()
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name
            };
        }
    }
}