using ZBC.Models;

namespace ZBC.Repository.Interfaces
{
    public interface IAddData
    {
        Task AddArticleAsync(Article article);
    }
}
