using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IArticleService
    {
        Task<dboArticle> Articles(dboArticle dboArticle);
        Task<bool> AddArticle(Article article);
        Task<bool> UpdateArticle(Article article);
        Task<Article> Article(Guid articleId);
    }
}
