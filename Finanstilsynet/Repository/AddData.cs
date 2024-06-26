﻿using Controllers;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;

namespace Repository
{
    public class AddData : IAddData
    {
        IServiceScopeFactory _serviceScopeFactory;
        public AddData(IServiceScopeFactory serviceScopeFactory) 
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task AddArticleAsync(Article article)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanstilsynetDBContext>();
                dbContext.Articles.Add(article);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddPcAsync(Pc pc)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanstilsynetDBContext>();

                var maxModelId = await dbContext.Products.MaxAsync(p => (int?)p.ModelId) ?? 0;
                int nextModelId = maxModelId + 1;

                var product = new Product
                {
                    ModelId = nextModelId,
                    MakerId = "A", 
                    ProductType = "pc" 
                };

                pc.ModelId = nextModelId; 
                pc.Model = product;

                dbContext.Pcs.Add(pc);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task AddPrinterAsync(Printer printer)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanstilsynetDBContext>();

                var maxModelId = await dbContext.Products.MaxAsync(p => (int?)printer.ModelId) ?? 0;
                int nextModelId = maxModelId + 1;

                var product = new Product
                {
                    ModelId = nextModelId,
                    MakerId = "A",
                };

                printer.ModelId = nextModelId;
                printer.Model = product;

                dbContext.Printers.Add(printer);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task AddLaptopAsync(Laptop laptop)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanstilsynetDBContext>();

                var maxModelId = await dbContext.Products.MaxAsync(p => (int?)p.ModelId) ?? 0;
                int nextModelId = maxModelId + 1;

                var product = new Product
                {
                    ModelId = nextModelId,
                };

                laptop.ModelId = nextModelId;
                laptop.Model = product;

                dbContext.Laptops.Add(laptop);
                await dbContext.SaveChangesAsync();
            }
        }

        Task IAddData.AddLaptopAsync(Laptops laptop)
        {
            throw new NotImplementedException();
        }

        Task IAddData.AddPrinterAsync(Printers printer)
        {
            throw new NotImplementedException();
        }
    }
}
