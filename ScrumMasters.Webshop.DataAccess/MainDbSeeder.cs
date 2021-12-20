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
                    ProductName = "Bluse", ProductPrice=250,ProductDescription = "Dette er en bluse",ProductFeatured = true, Categories = new List<CategoryEntity>(),
                    Sizes = new List<SizeEntity>(), Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                ProductEntity pe2 = new ProductEntity
                {
                    ProductName = "Kjole" ,ProductPrice=200,ProductDescription = "Dette er en smart kjole", Categories = new List<CategoryEntity>(), Sizes = new List<SizeEntity>(),
                    Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                ProductEntity pe3 = new ProductEntity
                {
                    ProductName = "Taske 1",ProductPrice=350,ProductDescription = "Dette er en taske", Categories = new List<CategoryEntity>(), Sizes = new List<SizeEntity>(),
                    Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                
                ProductEntity pe4 = new ProductEntity
                {
                    ProductName = "Taske 2",ProductPrice=350,ProductDescription = "Dette er en smart taske",ProductFeatured = true, Categories = new List<CategoryEntity>(), Sizes = new List<SizeEntity>(),
                    Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                
                ProductEntity pe5 = new ProductEntity
                {
                    ProductName = "Taske 3",ProductPrice=350,ProductDescription = "Dette er en smart taske", Categories = new List<CategoryEntity>(), Sizes = new List<SizeEntity>(),
                    Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                
                ProductEntity pe6 = new ProductEntity
                {
                    ProductName = "Bluse 2",ProductPrice=350,ProductDescription = "Dette er en smart bluse",ProductFeatured = true, Categories = new List<CategoryEntity>(), Sizes = new List<SizeEntity>(),
                    Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                
                ProductEntity pe7 = new ProductEntity
                {
                    ProductName = "Bluse 3",ProductDescription = "Dette er en bluse",ProductPrice=350, Categories = new List<CategoryEntity>(), Sizes = new List<SizeEntity>(),
                    Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                
                CategoryEntity ce1 = new CategoryEntity {Name = "Bluser"};
                CategoryEntity ce2 = new CategoryEntity {Name = "Kjoler"};
                CategoryEntity ce3 = new CategoryEntity {Name = "Tasker"};
                
                ColorEntity color1 = new ColorEntity {Title = "Rød",ColorString="#FF0000"};
                ColorEntity color2 = new ColorEntity {Title = "Blå",ColorString="#0000FF"};
                ColorEntity color3 = new ColorEntity {Title = "Gul",ColorString="#FFFF00"};
                ColorEntity color4 = new ColorEntity {Title = "Grøn",ColorString="#00FF00"};
                ColorEntity color5 = new ColorEntity {Title = "Grå",ColorString="#808080"};
                
                SizeEntity se1 = new SizeEntity {Title = "30/30"};
                SizeEntity se2 = new SizeEntity {Title = "25"};
                SizeEntity se3 = new SizeEntity {Title = "M"};
                
                ImageEntity ie1 = new ImageEntity
                {
                    Title = "Tøj1",
                    Path = "billed.png",
                };
                
                ImageEntity ie2 = new ImageEntity {
                    Title = "Kjole pink",
                    Path = "billed1.jpg",
                };
                
                ImageEntity ie3 = new ImageEntity
                {
                    Title = "Kjole sølv",
                    Path = "billed2.jpg",
                };
                
                ImageEntity ie4 = new ImageEntity
                {
                    Title = "Taske 1",
                    Path = "taske1.png",
                };
                
                ImageEntity ie5 = new ImageEntity
                {
                    Title = "Taske 2",
                    Path = "taske2.png",
                };
                
                ImageEntity ie6 = new ImageEntity
                {
                    Title = "Taske 3",
                    Path = "taske3.png",
                };
                
                ImageEntity ie7 = new ImageEntity {
                    Title = "bluse",
                    Path = "billed3.png",
                };
                
                ImageEntity ie8 = new ImageEntity
                {
                    Title = "bluse",
                    Path = "billed4.png",
                };
                
                pe1.Categories.Add(ce1);
                pe1.Categories.Add(ce2);
                pe1.Colors.Add(color1);
                pe1.Colors.Add(color5);
                pe1.Sizes.Add(se1);
                pe1.Images.Add(ie1);
                
                pe2.Categories.Add(ce2);
                pe2.Colors.Add(color1);
                pe2.Colors.Add(color4);
                pe2.Sizes.Add(se1);
                pe2.Sizes.Add(se2);
                pe2.Images.Add(ie2);
                pe2.Images.Add(ie3);
                
                pe3.Categories.Add(ce3);
                pe3.Colors.Add(color3);
                pe3.Colors.Add(color4);
                pe3.Sizes.Add(se3);
                pe3.Images.Add(ie4);
                
                pe4.Categories.Add(ce3);
                pe4.Colors.Add(color3);
                pe4.Colors.Add(color4);
                pe4.Images.Add(ie5);
                
                pe5.Categories.Add(ce3);
                pe5.Sizes.Add(se3);
                pe5.Images.Add(ie6);
                
                pe6.Categories.Add(ce1);
                pe6.Sizes.Add(se3);
                pe6.Images.Add(ie7);
                
                pe7.Categories.Add(ce1);
                pe7.Images.Add(ie8);
                
                
                mainContext.Products.AddRange(pe1, pe2, pe3,pe4,pe5,pe6,pe7);
                mainContext.SaveChanges();
        }

        public void SeedProduction()
        {
            mainContext.Database.EnsureCreated();
        }
    }
}