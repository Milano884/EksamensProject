using ZBC.Models;

namespace ZBC.Repository.Interfaces
{
    public interface IUpdateData
    {
        Task UpdateArticleAsync(Article article);
    }
}
