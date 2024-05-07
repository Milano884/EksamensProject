using Microsoft.EntityFrameworkCore;
using Finanstilsynet.Data;
using Finanstilsynet.Models;
using Finanstilsynet.Repository.Interfaces;

namespace Finanstilsynet.Repository
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
    }
}
