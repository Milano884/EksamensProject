using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ZBC.Data;
using ZBC.Models;
using ZBC.Repository.Interfaces;
using ZBC.ViewModels;

namespace ZBC.Repository
{
    public class GetData : IGetData
    {
        IServiceScopeFactory _serviceScopeFactory;

        public GetData(IServiceScopeFactory serviceScopeFactory) 
        {
            _serviceScopeFactory= serviceScopeFactory;
        }

        public async Task<bool> ArticleExistsAsync(int articleId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ZBCDBContext>();
                return await dbContext.Articles.AnyAsync(a => a.ArticleId == articleId);
            }
        }

        public async Task<Article> GetArticleByIdAsync(int articleID)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ZBCDBContext>();
                return await dbContext.Articles.FirstOrDefaultAsync(a => a.ArticleId == articleID);
            }
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ZBCDBContext>();
                
                return await dbContext.Articles.ToListAsync();
            }
        }

        public async Task<List<DataTableViewModel>> GetProductCatalogAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ZBCDBContext>();

                var query = dbContext.Products.Include(x=>x.Maker).Select(p => new DataTableViewModel
                {
                    ModelID = p.ModelId,
                    MakerID = p.MakerId,
                    Product_Type = p.ProductType,
                    Laptop_Speed = p.Laptop.Speed,
                    Laptop_RAM = p.Laptop.Ram,
                    Laptop_HardDisk = p.Laptop.HardDisk,
                    Laptop_Screen = p.Laptop.Screen.HasValue ? (int?)Convert.ToInt32(p.Laptop.Screen.Value) : null,
                    PC_Speed = p.Pc.Speed,
                    PC_RAM = p.Pc.Ram,
                    PC_HardDisk = p.Pc.HardDisk,
                    PC_ReadDrive = p.Pc.ReadDrive,
                    Printer_Color = p.Printer.Color,
                    Printer_Type = p.Printer.PrinterType,
                    Product_Price = (int?)p.Laptop.Price ?? p.Pc.Price ?? p.Printer.Price,
                    Maker_Color = p.Maker.MakerColor
                });

                List<DataTableViewModel> productCatalog = await query.ToListAsync();
                return productCatalog;
            }
        }

        public async Task<List<DashboardViewModel>> GetProductCatalogByMakerAsync()
        {
            var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ZBCDBContext>();

            var query = from p in dbContext.Products
                        join m in dbContext.Makers on p.MakerId equals m.MakerId
                        group new { p, m } by new { p.ProductType, m.MakerId, m.MakerColor } into grp
                        select new DashboardViewModel
                        {
                            ProductType = grp.Key.ProductType,
                            MakerID = grp.Key.MakerId,
                            MakerColor = grp.Key.MakerColor,
                            ProductCount = grp.Count()
                        };

            List<DashboardViewModel> productCatalogByMaker = query.ToList();
            return productCatalogByMaker ?? new();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ZBCDBContext>();

            return await dbContext.Products
                .Include(p => p.Laptop) 
                .Include(p => p.Pc)
                .Include(p => p.Printer)
                .Include(p => p.Maker)
                .ToListAsync();
        }
    }
}
