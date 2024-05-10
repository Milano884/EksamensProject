using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using Repository.Interfaces;

namespace Repository
{
    public class DeleteData : IDeleteData
    {
        IServiceScopeFactory _serviceScopeFactory;
        public DeleteData(IServiceScopeFactory serviceScopeFactory) 
        {
            _serviceScopeFactory= serviceScopeFactory;
        }

        public async Task ArticleAsync(int articleID)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanstilsynetDBContext>();
                var article = await dbContext.Articles.FindAsync(articleID);
                if (article != null)
                {
                    dbContext.Articles.Remove(article);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task ProductAsync(int modelID)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanstilsynetDBContext>();

                var pc = await dbContext.Pcs.FindAsync(modelID);


                var product = await dbContext.Products.FindAsync(modelID);
                if (product != null)
                {
                    dbContext.Pcs.Remove(pc);
                    await dbContext.SaveChangesAsync();

                    dbContext.Products.Remove(product);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}