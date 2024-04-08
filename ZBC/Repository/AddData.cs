using Microsoft.EntityFrameworkCore;
using ZBC.Data;
using ZBC.Models;
using ZBC.Repository.Interfaces;

namespace ZBC.Repository
{
    public class AddData : IAddData
    {
        IServiceScopeFactory _serviceScopeFactory;
        public AddData(IServiceScopeFactory serviceScopeFactory) 
        {
            _serviceScopeFactory= serviceScopeFactory;
        }

        public async Task AddArticleAsync(Article article)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ZBCDBContext>();
                dbContext.Articles.Add(article);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
