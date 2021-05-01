using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BS.Core;

namespace BS.Data.IRepositories
{
    public interface IArticleRepository
    {
        Task<dboArticle> Articles(dboArticle dboArticle);
        Task<bool> AddArticle(Article article);
        Task<bool> UpdateArticle(Article article);
        Task<Article> Article(Guid articleId);
    }
}
