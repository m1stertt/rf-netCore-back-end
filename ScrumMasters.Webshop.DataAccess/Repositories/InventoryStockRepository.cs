﻿using System.Collections.Generic;
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
                        Images = pe.Product.Images.Select(e =>new Image
                        {
                            Id = e.Id,
                            Path = e.Path
                        }).ToList(),
                        ProductPrice = pe.Product.ProductPrice,
                        ProductDiscountPrice = pe.Product.ProductDiscountPrice
                        //@todo
                    },
                    Color = new Color
                    {
                        Id = pe.Color.Id,
                        Title = pe.Color.Title,
                        ColorString = pe.Color.ColorString,
                        //@todo
                    },
                    Size = new Size
                    {
                        Id = pe.Color.Id,
                        Title = pe.Color.Title
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
            if (inventoryStock.Product.Id>0)
            {
                invStockEntity.Product = new ProductEntity {Id = inventoryStock.Product.Id};
            }
            if (inventoryStock.Color.Id>0)
            {
                invStockEntity.Color = new ColorEntity {Id = inventoryStock.Color.Id};
            }
            if (inventoryStock.Size.Id!=0)
            {
                invStockEntity.Size = new SizeEntity {Id = inventoryStock.Size.Id};
            }
            var savedEntity = _context.InventoryStock.Add(invStockEntity).Entity;
            _context.SaveChanges();
            return new InventoryStock()
            {
                Id = savedEntity.Id,
                Amount = savedEntity.Amount,
                //Product = savedEntity.Product,
                //Color = savedEntity.Color,
                //Size = savedEntity.Size,
            };
        }

        public InventoryStock Update(InventoryStock inventoryStock)
        {
            throw new System.NotImplementedException(); //@todo
        }

        public InventoryStock DeleteById(int id)
        {
            throw new System.NotImplementedException(); //@todo
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
                        Id = pe.Color.Id,
                        Title = pe.Color.Title
                    }
                }).Where(e=>e.Product.Id==id)
                .ToList();
        }
    }
}