namespace Repository.Interfaces
{
    public interface IDeleteData
    {
        Task ArticleAsync(int articleID);

        Task ProductAsync(int modelID);
    }
}
