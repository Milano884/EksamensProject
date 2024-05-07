using Finanstilsynet.Models;

namespace Finanstilsynet.Repository.Interfaces
{
    public interface IDeleteData
    {
        Task ArticleAsync(int articleID);
    }
}
