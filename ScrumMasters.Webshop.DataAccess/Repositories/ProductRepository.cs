using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.Domain.IRepositories;
using Size = ScrumMasters.Webshop.Core.Models.Size;
using Color = ScrumMasters.Webshop.Core.Models.Color;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{
    
    //https://github.com/dotnet/efcore/issues/22868
    //In regards to the SQL warning, seems to be first fixed in EF core 6.0 according to this github issue.
    //If we have the time, try to find a way around it for this version.
    public class ProductRepository : IProductRepository
    {
        private readonly MainDbContext _context;

        public ProductRepository(MainDbContext ctx)
        {
            _context = ctx ?? throw new InvalidDataException("Product Repository Must have a DBContext");
        }

        public List<Product> FindAll()
        {
            return _context.Products.Select(pe => new Product
                {
                    Id = pe.Id,
                    ProductName = pe.ProductName,
                    ProductPrice = pe.ProductPrice,
                    ProductDescription = pe.ProductDescription,
                    ProductImageUrl = pe.ProductImageUrl,
                    ProductFeatured = pe.ProductFeatured,
                    ProductDiscountPrice = pe.ProductDiscountPrice,
                    Categories = pe.Categories.Select(px=>new Category{Id = px.Id,Name = px.Name}).ToList(),
                    Sizes = pe.Sizes.Select(px=>new Size{Id = px.Id,Title = px.Title}).ToList(),
                    Colors = pe.Colors.Select(px=>new Color{Id = px.Id,Title = px.Title}).ToList(),
                    Images = pe.Images.Select(px=>new Image{Id = px.Id,Title = px.Title,Path=px.Path}).ToList(),
                })
                .ToList();
        }
        
        public PagedCategoryProductList<Product> GetPagedCategoriesProductList(CategoriesPaginationParameters categoriesPaginationParameters)
        {
            var values = categoriesPaginationParameters.ColorIds != null ? Array.ConvertAll(categoriesPaginationParameters.ColorIds.Split(new[] { ","},StringSplitOptions.RemoveEmptyEntries),
                x => { return Int32.Parse(x);}) : null;
            
            var query = _context.Products
                .Where(a => a.Categories
                    .Any(c => c.Id == categoriesPaginationParameters.categoryId));
            if (values is {Length: > 0})
            {
                query = query.Where(a => a.Colors.Any(c => values.Contains(c.Id)));
            }
            var select = query.Select(pe => new Product
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
                    
            var pagedList = PagedCategoryProductList<Product>.ToPagedList(select,
                categoriesPaginationParameters.PageNumber,
                categoriesPaginationParameters.PageSize,
                categoriesPaginationParameters.categoryId,
                values);
            return pagedList;
        }

        public PagedProductList<Product> GetPagedProductList(ProductPaginationParameters productParameters)
        {

            if (!string.IsNullOrEmpty(productParameters.SearchString))
            {
                var pagedList = PagedProductList<Product>.ToPagedList(FindAll().Where(product => product.ProductName.ToLower().Contains(productParameters.SearchString.ToLower())),
                    productParameters.PageNumber,
                    productParameters.PageSize,
                    productParameters.SearchString);
                return pagedList;
            }
            else
            {
                var pagedList = PagedProductList<Product>.ToPagedList(FindAll().OrderBy(product => product.ProductName),
                    productParameters.PageNumber,
                    productParameters.PageSize,
                    productParameters.SearchString);
                return pagedList;
            }

        }

        // public PagedCategoriesProductList<Product> GetPagedCategoriesProductList(CategoriesPaginationParameters categoriesPaginationParameters)
        // {
        //     if (categoriesPaginationParameters.Category != null)
        //     {
        //         var products = _context.Categories.Where(entity => entity.Id == categoriesPaginationParameters.Category.Id)
        //             .SelectMany(entity => entity.Product);
        //         
        //         var pagedList = PagedCategoriesProductList<Product>.ToPagedList(
        //             _context.Categories.Where(entity => entity.Id == categoriesPaginationParameters.Category.Id)
        //                 .SelectMany(entity => entity.Product),
        //             categoriesPaginationParameters.PageNumber,
        //             categoriesPaginationParameters.PageSize,
        //             categoriesPaginationParameters.Category.Name
        //             );
        //         // var pagedList = PagedCategoriesProductList<Product>.ToPagedList(FindAll().Where(product => product.Id.Equals(categoriesPaginationParameters.Category.Products.Where(product1 => product.Id == product1.Id))),
        //         //     categoriesPaginationParameters.PageNumber,
        //         //     categoriesPaginationParameters.PageSize,
        //         //     categoriesPaginationParameters.Category.Name);
        //         // return pagedList;
        //     }
        //     else
        //     {
        //         var pagedList = PagedCategoriesProductList<Product>.ToPagedList(FindAll().OrderBy(product => product.ProductName),
        //             categoriesPaginationParameters.PageNumber,
        //             categoriesPaginationParameters.PageSize,
        //             categoriesPaginationParameters.Category.Name);
        //         return pagedList;
        //     }
        // }

        public Product FindById(int id)
        {
            return _context.Products.Select(pe => new Product()
            {
                Id = pe.Id,
                ProductName = pe.ProductName,
                ProductPrice = pe.ProductPrice,
                ProductDescription = pe.ProductDescription,
                ProductImageUrl = pe.ProductImageUrl,
                ProductFeatured = pe.ProductFeatured,
                ProductDiscountPrice = pe.ProductDiscountPrice,
                Categories = pe.Categories.Select(px=>new Category{Id = px.Id,Name = px.Name}).ToList(),
                Sizes = pe.Sizes.Select(px=>new Size{Id = px.Id,Title = px.Title}).ToList(),
                Colors = pe.Colors.Select(px=>new Color{Id = px.Id,Title = px.Title,ColorString = px.ColorString}).ToList(),
                Images = pe.Images.Select(px=>new Image{Id = px.Id,Title = px.Title,Path=px.Path}).ToList(),
            }).FirstOrDefault(product => product.Id == id);
        }

        public Product Create(Product product)
        {
            var productEntity = new ProductEntity()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                ProductImageUrl = product.ProductImageUrl,
                ProductDiscountPrice = product.ProductDiscountPrice,
                ProductFeatured = product.ProductFeatured,
            };
            var savedEntity = _context.Products.Add(productEntity).Entity;
            _context.SaveChanges();
            return new Product()
            {
                Id = product.Id,
                ProductName = savedEntity.ProductName,
                ProductPrice = savedEntity.ProductPrice,
                ProductDescription = savedEntity.ProductDescription,
                ProductImageUrl = savedEntity.ProductImageUrl,
                ProductFeatured = savedEntity.ProductFeatured,
                ProductDiscountPrice = savedEntity.ProductDiscountPrice,
            };
        }

        public Product Update(Product product)
        {
            ProductEntity edit= _context.Products.Include("Categories").Include("Colors").Include("Sizes").Single(productt => productt.Id == product.Id);
            edit.Categories.Clear();
            edit.Colors.Clear();
            edit.Sizes.Clear();
            edit.ProductName = product.ProductName;
            edit.ProductPrice = product.ProductPrice;
            edit.ProductDescription = product.ProductDescription;
            edit.ProductImageUrl = product.ProductImageUrl;
            edit.ProductFeatured = product.ProductFeatured;
            edit.ProductDiscountPrice = product.ProductDiscountPrice;
            edit.Categories = product.Categories.Select(px =>_context.Categories.Single(pe=>px.Id==pe.Id)).ToList();
            edit.Sizes = product.Sizes.Select(px =>_context.Sizes.Single(pe=>px.Id==pe.Id)).ToList();
            edit.Colors = product.Colors.Select(px =>_context.Colors.Single(pe=>px.Id==pe.Id)).ToList();
            _context.SaveChanges();
            return product;
        }

        public Product DeleteById(int id)
        {
            var savedEntity = _context.Products.Remove(new ProductEntity() {Id = id}).Entity;
            _context.SaveChanges();
            return new Product()
            {
                Id = savedEntity.Id,
                ProductName = savedEntity.ProductName,
                ProductPrice = savedEntity.ProductPrice,
                ProductDescription = savedEntity.ProductDescription,
                ProductImageUrl = savedEntity.ProductImageUrl,
                ProductFeatured = savedEntity.ProductFeatured,
                ProductDiscountPrice = savedEntity.ProductDiscountPrice,
            };
        }

        public List<Product> GetFeaturedProducts()
        {
            return _context.Products.Select(pe => new Product()
            {
                Id = pe.Id,
                ProductName = pe.ProductName,
                ProductPrice = pe.ProductPrice,
                ProductDescription = pe.ProductDescription,
                ProductImageUrl = pe.ProductImageUrl,
                ProductFeatured = pe.ProductFeatured,
                ProductDiscountPrice = pe.ProductDiscountPrice,
                Categories = pe.Categories.Select(px=>new Category{Id = px.Id,Name = px.Name}).ToList(),
                Sizes = pe.Sizes.Select(px=>new Size{Id = px.Id,Title = px.Title}).ToList(),
                Colors = pe.Colors.Select(px=>new Color{Id = px.Id,Title = px.Title}).ToList(),
                Images = pe.Images.Select(px=>new Image{Id = px.Id,Title = px.Title,Path=px.Path}).ToList(),
            }).Where(product => product.ProductFeatured == true).ToList();
        }
    }
}