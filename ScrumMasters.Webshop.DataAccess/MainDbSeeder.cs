using System.Collections.Generic;
using ScrumMasters.Webshop.DataAccess.Entities;

namespace ScrumMasters.Webshop.DataAccess
{
    public class MainDbSeeder : IMainDbSeeder
    {
        private readonly MainDbContext mainContext;

        public MainDbSeeder(MainDbContext ctx)
        {
            mainContext = ctx;
        }

        public void SeedDevelopment()
        {

                mainContext.Database.EnsureDeleted();
                mainContext.Database.EnsureCreated();
                mainContext.SaveChanges();
                ProductEntity pe1 = new ProductEntity
                {
                    ProductName = "P1", ProductPrice=250,ProductFeatured = true, Categories = new List<CategoryEntity>(),
                    Sizes = new List<SizeEntity>(), Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                ProductEntity pe2 = new ProductEntity
                {
                    ProductName = "P2" ,ProductPrice=200, Categories = new List<CategoryEntity>(), Sizes = new List<SizeEntity>(),
                    Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                ProductEntity pe3 = new ProductEntity
                {
                    ProductName = "P3",ProductPrice=350, Categories = new List<CategoryEntity>(), Sizes = new List<SizeEntity>(),
                    Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                
                CategoryEntity ce1 = new CategoryEntity {Name = "Bukser"};
                CategoryEntity ce2 = new CategoryEntity {Name = "Sko"};
                CategoryEntity ce3 = new CategoryEntity {Name = "Kjoler"};
                
                ColorEntity color1 = new ColorEntity {Title = "Rød",ColorString="#FF0000"};
                ColorEntity color2 = new ColorEntity {Title = "Blå",ColorString="#0000FF"};
                ColorEntity color3 = new ColorEntity {Title = "Gul",ColorString="#FFFF00"};
                ColorEntity color4 = new ColorEntity {Title = "Grøn",ColorString="#00FF00"};
                ColorEntity color5 = new ColorEntity {Title = "Grå",ColorString="#808080"};
                
                SizeEntity se1 = new SizeEntity {Title = "30/30"};
                SizeEntity se2 = new SizeEntity {Title = "25"};
                SizeEntity se3 = new SizeEntity {Title = "30"};
                
                ImageEntity ie1 = new ImageEntity
                {
                    Title = "Some title",
                    Path = "test2.jpg",
                };
                
                ImageEntity ie2 = new ImageEntity {
                    Title = "Some title34",
                    Path = "test1.jpg",
                };
                
                ImageEntity ie3 = new ImageEntity
                {
                    Title = "Some title",
                    Path = "billed1.jpg",
                };
                
                ImageEntity ie4 = new ImageEntity
                {
                    Title = "Some title",
                    Path = "billed2.jpg",
                };
                
                pe1.Categories.Add(ce1);
                pe1.Categories.Add(ce2);
                pe1.Colors.Add(color1);
                pe1.Colors.Add(color5);
                pe1.Sizes.Add(se1);
                pe1.Images.Add(ie1);
                pe1.Images.Add(ie4);
                
                pe2.Categories.Add(ce2);
                pe2.Colors.Add(color2);
                pe2.Colors.Add(color1);
                pe2.Sizes.Add(se1);
                pe2.Sizes.Add(se2);
                pe2.Images.Add(ie2);
                
                pe3.Categories.Add(ce3);
                pe3.Colors.Add(color3);
                pe3.Colors.Add(color4);
                pe3.Sizes.Add(se3);
                pe3.Images.Add(ie3);
                
                
                mainContext.Products.AddRange(pe1, pe2, pe3);
                mainContext.SaveChanges();
        }

        public void SeedProduction()
        {
            // For now. Should be fixed for production ready code.
                mainContext.Database.EnsureDeleted();
                
                mainContext.Database.EnsureCreated();
                mainContext.SaveChanges();
                ProductEntity pe1 = new ProductEntity
                {
                    ProductName = "P1", ProductPrice=250,ProductFeatured = true, Categories = new List<CategoryEntity>(),
                    Sizes = new List<SizeEntity>(), Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                ProductEntity pe2 = new ProductEntity
                {
                    ProductName = "P2" ,ProductPrice=200, Categories = new List<CategoryEntity>(), Sizes = new List<SizeEntity>(),
                    Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                ProductEntity pe3 = new ProductEntity
                {
                    ProductName = "P3",ProductPrice=350, Categories = new List<CategoryEntity>(), Sizes = new List<SizeEntity>(),
                    Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                
                CategoryEntity ce1 = new CategoryEntity {Name = "Bukser"};
                CategoryEntity ce2 = new CategoryEntity {Name = "Sko"};
                CategoryEntity ce3 = new CategoryEntity {Name = "Kjoler"};
                
                ColorEntity color1 = new ColorEntity {Title = "Rød",ColorString="#FF0000"};
                ColorEntity color2 = new ColorEntity {Title = "Blå",ColorString="#0000FF"};
                ColorEntity color3 = new ColorEntity {Title = "Gul",ColorString="#FFFF00"};
                ColorEntity color4 = new ColorEntity {Title = "Grøn",ColorString="#00FF00"};
                ColorEntity color5 = new ColorEntity {Title = "Grå",ColorString="#808080"};
                
                SizeEntity se1 = new SizeEntity {Title = "30/30"};
                SizeEntity se2 = new SizeEntity {Title = "25"};
                SizeEntity se3 = new SizeEntity {Title = "30"};
                
                ImageEntity ie1 = new ImageEntity
                {
                    Title = "Some title",
                    Path = "test2.jpg",
                };
                
                ImageEntity ie2 = new ImageEntity {
                    Title = "Some title34",
                    Path = "test1.jpg",
                };
                
                ImageEntity ie3 = new ImageEntity
                {
                    Title = "Some title",
                    Path = "billed1.jpg",
                };
                
                ImageEntity ie4 = new ImageEntity
                {
                    Title = "Some title",
                    Path = "billed2.jpg",
                };
                
                pe1.Categories.Add(ce1);
                pe1.Categories.Add(ce2);
                pe1.Colors.Add(color1);
                pe1.Colors.Add(color5);
                pe1.Sizes.Add(se1);
                pe1.Images.Add(ie1);
                pe1.Images.Add(ie4);
                
                pe2.Categories.Add(ce2);
                pe2.Colors.Add(color2);
                pe2.Colors.Add(color1);
                pe2.Sizes.Add(se1);
                pe2.Sizes.Add(se2);
                pe2.Images.Add(ie2);
                
                pe3.Categories.Add(ce3);
                pe3.Colors.Add(color3);
                pe3.Colors.Add(color4);
                pe3.Sizes.Add(se3);
                pe3.Images.Add(ie3);
                
                
                mainContext.Products.AddRange(pe1, pe2, pe3);
                mainContext.SaveChanges();
        }
    }
}