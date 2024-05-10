using Models;

namespace ViewModels
{
    public class ArticleViewModel
    {
        public Article Article { get; set; } = new Article();
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
