using Models;
using ViewModels;

namespace Repository.Interfaces
{
    public interface IGetData
    {
        Task<bool> ArticleExistsAsync(int articleID);
        Task<Article> GetArticleByIdAsync(int articleID);
        Task<List<Article>> GetAllArticlesAsync();
        Task<List<Product>> GetAllProductsAsync();
        Task<List<ProductViewModel>> GetProductCatalogAsync();
        Task<List<DashboardViewModel>> GetProductCatalogByMakerAsync();
        Task<Pc> GetProductByModelIDAsync(int modelID);
    }
}
