using System.Collections.Generic;
using System.IO;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.Domain.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new InvalidDataException("ProductRepository Cannot Be Null");
        }
        public PagedProductList<Product> GetPagedProductList(ProductPaginationParameters productParameters)
        {
            return _productRepository.GetPagedProductList(productParameters);
        }
        public PagedCategoryProductList<Product> GetPagedCategoryProducts(CategoriesPaginationParameters categoriesPaginationParameters)
        {
            return _productRepository.GetPagedCategoriesProductList(categoriesPaginationParameters);
        }

        public Product Create(Product product)
        {
            return _productRepository.Create(product);
        }

        public Product GetProductById(int id)
        {
            return _productRepository.FindById(id);
        }

        public Product Update(Product product)
        {
            return _productRepository.Update(product);
        }

        public Product DeleteById(int id)
        {
            return  _productRepository.DeleteById(id);
        }

        public List<Product> GetFeaturedProducts()
        {
            return _productRepository.GetFeaturedProducts();
        }
    }
    }
