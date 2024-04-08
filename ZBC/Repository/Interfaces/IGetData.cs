using Microsoft.AspNetCore.Mvc;
using ZBC.Models;
using ZBC.ViewModels;

namespace ZBC.Repository.Interfaces
{
    public interface IGetData
    {
        Task<bool> ArticleExistsAsync(int articleID);
        Task<Article> GetArticleByIdAsync(int articleID);
        Task<List<Article>> GetAllArticlesAsync();
        Task<List<Product>> GetAllProductsAsync();
        Task<List<DataTableViewModel>> GetProductCatalogAsync();
        Task<List<DashboardViewModel>> GetProductCatalogByMakerAsync();
    }
}
