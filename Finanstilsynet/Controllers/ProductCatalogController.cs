using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using Repository.Interfaces;
using System.Diagnostics;
using ViewModels;
using static System.Formats.Asn1.AsnWriter;

namespace Controllers
{
    public class ProductCatalogController : Controller
    {
        private readonly ILogger<ProductCatalogController> _logger;

        private readonly IGetData _getData;
        private readonly IAddData _addData;
        private readonly IDeleteData _deleteData;
        private readonly IUpdateData _updateData;

        public ProductCatalogController(ILogger<ProductCatalogController> logger, IAddData addData, IDeleteData deleteData, IUpdateData updateData, IGetData getData)
        {
            _addData = addData;
            _deleteData = deleteData;
            _updateData = updateData;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> PersonalComputers()
        {
            var productCatalog = await _getData.GetProductCatalogAsync();
            return View(productCatalog);
        }

        public async Task<IActionResult> EditPersonalComputer(int? modelID)
        {
            if (modelID == null)
            {
                return NotFound();
            }

            var product = await _getData.GetProductByModelIDAsync(modelID.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View("PersonalComputer", product);
        }

        public async Task<IActionResult> DeletePersonalComputer(int? modelID)
        {
            if (modelID == null)
            {
                return NotFound();
            }

            await _deleteData.ProductAsync(modelID.Value);
            return RedirectToAction(nameof(PersonalComputers));
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewPersonalComputer([FromForm] Pc pc)
        {
            if (pc == null)
            {
                return NotFound();
            }

            await _addData.AddPcAsync(pc);
            return RedirectToAction(nameof(PersonalComputers));
        }

        public async Task<IActionResult> NewPersonalComputer()
        {
            var model = new Pc{};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePc([FromForm] Pc pc)
        {
            if (pc == null)
            {
                return NotFound();
            }

            try
            {
                await _updateData.UpdatePcAsync(pc);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the PC." + ex.Message);
                return View(pc);
            }

            return RedirectToAction(nameof(PersonalComputers));
        }
    }
}
