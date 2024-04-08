using ZBC.Models;

namespace ZBC.Repository.Interfaces
{
    public interface IDeleteData
    {
        Task ArticleAsync(int articleID);
    }
}
