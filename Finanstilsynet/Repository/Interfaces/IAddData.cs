using Models;

namespace Repository.Interfaces
{
    public interface IAddData
    {
        Task AddArticleAsync(Article article);

        Task AddPcAsync(Pc pc);
    }
}
