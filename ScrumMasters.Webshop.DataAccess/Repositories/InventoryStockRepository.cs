using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{
    public class InventoryStockRepository : IInventoryStockRepository
    {
        private readonly MainDbContext _context;

        public InventoryStockRepository(MainDbContext ctx)
        {
            _context = ctx ?? throw new InvalidDataException("Product Repository Must have a DBContext");
        }

        public InventoryStock FindById(int id)
        {
            return _context.InventoryStock
                .Select(pe=>new InventoryStock
                {
                    Id = pe.Id,
                    Amount = pe.Amount,
                    Product = new Product
                    {
                        Id = pe.Product.Id,
                        ProductName = pe.Product.ProductName,
                        ProductPrice = pe.Product.ProductPrice,
                        ProductDiscountPrice = pe.Product.ProductDiscountPrice
                    },
                    Color = new Color
                    {
                        Id = pe.Color.Id,
                        Title = pe.Color.Title,
                        ColorString = pe.Color.ColorString,
                    },
                    Size = new Size
                    {
                        Id = pe.Size.Id,
                        Title = pe.Size.Title
                    }
                }).FirstOrDefault(e => e.Id==id);
        }

        public InventoryStock Create(InventoryStock inventoryStock)
        {
            var invStockEntity = new InventoryStockEntity()
            {
                Id = inventoryStock.Id,
                Amount = inventoryStock.Amount,
            };
            if (inventoryStock.Product.Id > 0)
            {
                invStockEntity.Product = _context.Products.FirstOrDefault(r => r.Id == inventoryStock.Product.Id);
            }
            else return null;
            if (inventoryStock.Color.Id>0)
            {
                invStockEntity.Color = _context.Colors.FirstOrDefault(r=>r.Id==inventoryStock.Color.Id);
            }
            if (inventoryStock.Size.Id!=0)
            {
                invStockEntity.Size = _context.Sizes.FirstOrDefault(r=>r.Id==inventoryStock.Size.Id);
            }
            var pe = _context.InventoryStock.Add(invStockEntity).Entity;
            _context.SaveChanges();
            return new InventoryStock()
            {
                Id = pe.Id,
                Amount = pe.Amount,
                Product = new Product
                {
                    Id = pe.Product.Id,
                },
                Color = new Color
                {
                    Id = pe.Color.Id,
                    Title = pe.Color.Title,
                    ColorString = pe.Color.ColorString,
                },
                Size = new Size
                {
                    Id = pe.Size.Id,
                    Title = pe.Size.Title
                }
            };
        }

        public InventoryStock Update(InventoryStock inventoryStock)
        {
            if (inventoryStock == null) return null;
            var savedEntity = _context.Update(new InventoryStockEntity()
            {
                Id = inventoryStock.Id,
                Amount = inventoryStock.Amount,
            }).Entity;
            _context.SaveChanges();
            return new InventoryStock()
            {
                Id = savedEntity.Id,
                Amount = savedEntity.Amount,
            };
        }

        public InventoryStock DeleteById(int id)
        {
            if (id <= 0) return null;
            var pe = _context.InventoryStock.Remove(new InventoryStockEntity() {Id = id}).Entity;
            _context.SaveChanges();
            return new InventoryStock()
            {
                Id = pe.Id,
                Amount = pe.Amount,
                Product = new Product
                {
                    Id = pe.Product.Id,
                },
                Color = new Color
                {
                    Id = pe.Color.Id,
                    Title = pe.Color.Title,
                    ColorString = pe.Color.ColorString,
                },
                Size = new Size
                {
                    Id = pe.Size.Id,
                    Title = pe.Size.Title
                }
            };
        }

        public List<InventoryStock> FindByProductId(int id)
        {
            return _context.InventoryStock.Select(pe => new InventoryStock
                {
                    Id = pe.Id,
                    Amount = pe.Amount,
                    Product = new Product
                    {
                        Id = pe.Product.Id,
                    },
                    Color = new Color
                    {
                        Id = pe.Color.Id,
                        Title = pe.Color.Title,
                        ColorString = pe.Color.ColorString,
                    },
                    Size = new Size
                    {
                        Id = pe.Size.Id,
                        Title = pe.Size.Title
                    }
                }).Where(e=>e.Product.Id==id)
                .ToList();
        }
    }
}