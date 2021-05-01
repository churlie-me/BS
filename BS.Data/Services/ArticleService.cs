using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class ArticleService : IArticleService
    {
        private IArticleRepository _articleRepository;
        public ArticleService(IArticleRepository _articleRepository)
        {
            this._articleRepository = _articleRepository;
        }

        public async Task<bool> AddArticle(Article article)
        {
            return await _articleRepository.AddArticle(article);
        }

        public async Task<Article> Article(Guid articleId)
        {
            return await _articleRepository.Article(articleId);
        }

        public async Task<dboArticle> Articles(dboArticle dboArticle)
        {
            return await _articleRepository.Articles(dboArticle);
        }

        public async Task<bool> UpdateArticle(Article article)
        {
            return await _articleRepository.UpdateArticle(article);
        }
    }
}
