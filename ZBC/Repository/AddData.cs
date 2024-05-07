using Microsoft.EntityFrameworkCore;
using Finanstilsynet.Data;
using Finanstilsynet.Models;
using Finanstilsynet.Repository.Interfaces;

namespace Finanstilsynet.Repository
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
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanstilsynetDBContext>();
                dbContext.Articles.Add(article);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
