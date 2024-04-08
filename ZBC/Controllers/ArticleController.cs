using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ZBC.Models;
using ZBC.Repository.Interfaces;

namespace ZBC.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly IAddData _addData;
        private readonly IDeleteData _deleteData;
        private readonly IUpdateData _updateData;
        private readonly IGetData _getData;

        public ArticleController(IAddData addData, IDeleteData deleteData, IUpdateData updateData, IGetData getData)
        {
            _addData = addData;
            _deleteData = deleteData;
            _updateData = updateData;
            _getData = getData;
        }
        public async Task<IActionResult> Articles()
        {
            var articles = await _getData.GetAllArticlesAsync();

            return View(articles);
        }
        public async Task<IActionResult> Index() 
        {
            var articles = await _getData.GetAllArticlesAsync();
            return View("Articles", articles); 
        }
        public async Task<IActionResult> Details(int articleID)
        {
            var article = await _getData.GetArticleByIdAsync(articleID);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }


        public async Task<IActionResult> Edit(int? articleId)
        {
            if (articleId == null)
            {
                return NotFound();
            }

            var article = await _getData.GetArticleByIdAsync(articleId.Value); 
            if (article == null)
            {
                return NotFound();
            }
            return View("Article", article);
        }
       
        
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] Article article)
        {
            if (article == null)
            {
                return NotFound();
            }

            await _updateData.UpdateArticleAsync(article);
            if (article == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> NewArticle()
        {
            var model = new Article{ PublicationDateTime = DateTime.Today };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveNewArticle([FromForm] Article article)
        {
            if (article == null)
            {
                return NotFound();
            }

            await _addData.AddArticleAsync(article);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? articleId)
        {
            if (articleId == null)
            {
                return NotFound();
            }

            await _deleteData.ArticleAsync(articleId.Value);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
