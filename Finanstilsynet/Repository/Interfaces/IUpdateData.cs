using Models;

namespace Repository.Interfaces
{
    public interface IUpdateData
    {
        Task UpdateArticleAsync(Article article);

        Task UpdatePcAsync(Pc pc);
    }
}
