using Microsoft.EntityFrameworkCore;
using Finanstilsynet.Data;
using Finanstilsynet.Models;
using Finanstilsynet.Repository.Interfaces;

namespace Finanstilsynet.Repository
{
    public class UpdateData : IUpdateData
    {
        IServiceScopeFactory _serviceScopeFactory;
        public UpdateData(IServiceScopeFactory serviceScopeFactory) 
        {
            _serviceScopeFactory= serviceScopeFactory;
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
    }
}
