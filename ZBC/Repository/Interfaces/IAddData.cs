using Finanstilsynet.Models;

namespace Finanstilsynet.Repository.Interfaces
{
    public interface IAddData
    {
        Task AddArticleAsync(Article article);
    }
}
