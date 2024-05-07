using Finanstilsynet.Models;

namespace Finanstilsynet.Repository.Interfaces
{
    public interface IUpdateData
    {
        Task UpdateArticleAsync(Article article);
    }
}
