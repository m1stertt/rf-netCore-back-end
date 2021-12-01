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
        public List<Product> GetProducts()
        {
            return _productRepository.FindAll();
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
    }
    }
