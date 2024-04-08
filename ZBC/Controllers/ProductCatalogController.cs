using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZBC.Models;
using ZBC.Repository.Interfaces;

namespace ZBC.Controllers
{
    public class ProductCatalogController : Controller
    {
        private readonly ILogger<ProductCatalogController> _logger;
        private readonly IGetData _getData;

        public ProductCatalogController(ILogger<ProductCatalogController> logger, IGetData getData)
        {
            _logger = logger;
            _getData = getData;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var productCatalog = await _getData.GetProductCatalogAsync();
            return View(productCatalog);
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var productCatalogByMaker = await _getData.GetProductCatalogByMakerAsync();
            return View(productCatalogByMaker);
        }

        [Authorize]
        public IActionResult Playground()
        {
            var products = _getData.GetAllProductsAsync().Result; 
            if (products == null)
            {
                products = new List<Product>();
            }

            return View(products);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
