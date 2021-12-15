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
                    Products = pe.Product.Select(px=>new Product{Id = px.Id,ProductName = px.ProductName}).ToList()
                })
                .ToList();
        }
        
        public PagedCategoriesProductList<Product> GetPagedCategoriesProductList(CategoriesPaginationParameters categoriesPaginationParameters)
        {
            var query = context.Products
                    .Where(a => a.Categories
                        .Any(c => c.Id == categoriesPaginationParameters.categoryId)).Select(pe => new Product
                    {
                        Id = pe.Id,
                        ProductName = pe.ProductName,
                        ProductPrice = pe.ProductPrice,
                        ProductDescription = pe.ProductDescription,
                        ProductImageUrl = pe.ProductImageUrl,
                        ProductFeatured = pe.ProductFeatured,
                        Categories = pe.Categories.Select(px=>new Category{Id = px.Id,Name = px.Name}).ToList(),
                        Sizes = pe.Sizes.Select(px=>new Size{Id = px.Id,Title = px.Title}).ToList(),
                        Colors = pe.Colors.Select(px=>new Color{Id = px.Id,Title = px.Title}).ToList(),
                        Images = pe.Images.Select(px=>new Image{Id = px.Id,Title = px.Title,Path=px.Path}).ToList(),
                    })
                    .ToList();
                    
                var pagedList1 = PagedCategoriesProductList<Product>.ToPagedList(query,
                categoriesPaginationParameters.PageNumber,
                categoriesPaginationParameters.PageSize,
                categoriesPaginationParameters.categoryId);
                return pagedList1;
        }

        public Category GetById(int id)
        {
            return context.Categories
                .Select(pe => new Category
                {
                    Id = pe.Id,
                    Name = pe.Name,
                    Products = pe.Product.Select(px=>new Product{Id = px.Id,ProductName = px.ProductName}).ToList()
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