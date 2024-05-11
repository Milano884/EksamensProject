using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;

namespace Repository
{
    public class UpdateData : IUpdateData
    {
        IServiceScopeFactory _serviceScopeFactory;
        public UpdateData(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task UpdateArticleAsync(Article article)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanstilsynetDBContext>();

                var existingArticle = await dbContext.Articles.FirstOrDefaultAsync(a => a.ArticleId == article.ArticleId);
                if (existingArticle != null)
                {
                    dbContext.Entry(existingArticle).CurrentValues.SetValues(article);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task UpdatePcAsync(Pc pc)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanstilsynetDBContext>();

                var existingPc = await dbContext.Pcs.FirstOrDefaultAsync(a => a.ModelId == pc.ModelId);
                if (existingPc != null)
                {
                    dbContext.Entry(existingPc).CurrentValues.SetValues(pc);
                    dbContext.Entry(existingPc).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        public async Task UpdatePrinterAsync(Printer printer)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanstilsynetDBContext>();

                var existingArticle = await dbContext.Printers.FirstOrDefaultAsync(a => a.ModelId == printer.ModelId);
                if (existingArticle != null)
                {
                    dbContext.Entry(existingArticle).CurrentValues.SetValues(printer);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        public async Task UpdateLaptopAsync(Laptop laptop)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanstilsynetDBContext>();

                var existingArticle = await dbContext.Printers.FirstOrDefaultAsync(a => a.ModelId == laptop.ModelId);
                if (existingArticle != null)
                {
                    dbContext.Entry(existingArticle).CurrentValues.SetValues(laptop);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
