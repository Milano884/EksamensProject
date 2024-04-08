using System;
using System.Collections.Generic;

namespace ZBC.Models;

public partial class Article
{
    public int ArticleId { get; set; }

    public string? ArticleTitle { get; set; }

    public string? ArticleDescription { get; set; }

    public string? ArticleCategory { get; set; }

    public DateTime PublicationDateTime { get; set; }

    public string? ArticleSummary { get; set; }
}
