using Microsoft.EntityFrameworkCore;
using ZBC.Data;
using ZBC.Models;
using ZBC.Repository.Interfaces;

namespace ZBC.Repository
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
                var dbContext = scope.ServiceProvider.GetRequiredService<ZBCDBContext>();

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
