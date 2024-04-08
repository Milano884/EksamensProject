using Microsoft.EntityFrameworkCore;
using ZBC.Data;
using ZBC.Models;
using ZBC.Repository.Interfaces;

namespace ZBC.Repository
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
                var dbContext = scope.ServiceProvider.GetRequiredService<ZBCDBContext>();
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
