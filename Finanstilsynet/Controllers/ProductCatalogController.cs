using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using Repository.Interfaces;
using System.Diagnostics;
using System.Reflection;
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

        public IActionResult Info()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

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
            var model = new Pc { };
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

        [Authorize]
        public async Task<IActionResult> Printers()
        {
            var productCatalog = await _getData.GetProductCatalogAsync();
            return View(productCatalog);
        }

        public async Task<IActionResult> EditPrinter(int? modelID)
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
            return View("Printer", product);
        }

        public async Task<IActionResult> DeletePrinter(int? modelID)
        {
            if (modelID == null)
            {
                return NotFound();
            }

            await _deleteData.ProductAsync(modelID.Value);
            return RedirectToAction(nameof(Printers));
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewPrinter([FromForm] Printers printer)
        {
            if (printer == null)
            {
                return NotFound();
            }

            await _addData.AddPrinterAsync(printer);
            return RedirectToAction(nameof(Printers));
        }

        public async Task<IActionResult> NewPrinter()
        {
            var model = new Printer { };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePrinter([FromForm] Printer printer)
        {
            if (printer == null)
            {
                return NotFound();
            }

            try
            {
                await _updateData.UpdatePrinterAsync(printer);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the Printer." + ex.Message);
                return View(printer);
            }

            return RedirectToAction(nameof(Printers));
        }
        [Authorize]
        public async Task<IActionResult> Laptops()
        {
            var productCatalog = await _getData.GetProductCatalogAsync();
            return View(productCatalog);
        }

        public async Task<IActionResult> EditLaptops(int? modelID)
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
            return View("Laptop", product);
        }

        public async Task<IActionResult> DeleteLaptop(int? modelID)
        {
            if (modelID == null)
            {
                return NotFound();
            }

            await _deleteData.ProductAsync(modelID.Value);
            return RedirectToAction(nameof(Laptops));
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewLaptop([FromForm] Laptop laptop)
        {
            if (laptop == null)
            {
                return NotFound();
            }

            await _addData.AddLaptopAsync(laptop);
            return RedirectToAction(nameof(Laptops));
        }

        public async Task<IActionResult> NewLaptop()
        {
            var model = new Laptop { };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLaptop([FromForm] Laptop laptop)
        {
            if (laptop == null)
            {
                return NotFound();
            }

            try
            {
                await _updateData.UpdateLaptopAsync(laptop);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the Laptop." + ex.Message);
                return View(laptop);
            }

            return RedirectToAction(nameof(Laptops));
        }
    }
}
public class Printers
    {
    }
public class Laptops
    {
    }
