using BS.Core;
using BS.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        #region Private fields & constructor
        private readonly BSDBContext _bsdbContext;

        public ArticleRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        #endregion

        public async Task<dboArticle> Articles(dboArticle dboArticle)
        {
            if (!string.IsNullOrEmpty(dboArticle.search) && (dboArticle.CategoryId != Guid.Empty || dboArticle.BrandId != Guid.Empty))
            {
                dboArticle.Products = await _bsdbContext.Article.Include(s => s.SaleItem)
                                                 .Include(a => a.Category)
                                                 .Include(a => a.Brand)
                                                 .Include(p => p.Instructions.Where(i => !i.Deleted))
                                                 .Where(y => y.Name.Contains(dboArticle.search) && (y.BrandId == dboArticle.BrandId || y.CategoryId == dboArticle.CategoryId) && !y.Deleted)
                                                 .Skip(dboArticle.pageSize * (dboArticle.page - 1))
                                                 .Take(dboArticle.pageSize)
                                                 .ToListAsync();

                dboArticle.pageCount = (int)Math.Ceiling((double)_bsdbContext.Article.Where(y => y.Name.Contains(dboArticle.search) && (y.BrandId == dboArticle.BrandId || y.CategoryId == dboArticle.CategoryId) && !y.Deleted).Count() / dboArticle.pageSize);
            }
            else if (string.IsNullOrEmpty(dboArticle.search) && (dboArticle.CategoryId != Guid.Empty || dboArticle.BrandId != Guid.Empty))
            {
                dboArticle.Products = await _bsdbContext.Article.Include(s => s.SaleItem)
                                                 .Include(a => a.Category)
                                                 .Include(a => a.Brand)
                                                 .Include(p => p.Instructions.Where(i => !i.Deleted))
                                                 .Where(y => (y.BrandId == dboArticle.BrandId || y.CategoryId == dboArticle.CategoryId) && !y.Deleted)
                                                 .Skip(dboArticle.pageSize * (dboArticle.page - 1))
                                                 .Take(dboArticle.pageSize)
                                                 .ToListAsync();

                dboArticle.pageCount = (int)Math.Ceiling((double)_bsdbContext.Article.Where(y => (y.BrandId == dboArticle.BrandId || y.CategoryId == dboArticle.CategoryId) && !y.Deleted).Count() / dboArticle.pageSize);
            }
            else if (!string.IsNullOrEmpty(dboArticle.search) && dboArticle.CategoryId == Guid.Empty && dboArticle.BrandId == Guid.Empty)
            {
                {
                    dboArticle.Products = await _bsdbContext.Article.Include(s => s.SaleItem)
                                                     .Include(a => a.Category)
                                                     .Include(a => a.Brand)
                                                     .Include(p => p.Instructions.Where(i => !i.Deleted))
                                                     .Where(y => y.Name.Contains(dboArticle.search) && !y.Deleted)
                                                     .Skip(dboArticle.pageSize * (dboArticle.page - 1))
                                                     .Take(dboArticle.pageSize)
                                                     .ToListAsync();

                    dboArticle.pageCount = (int)Math.Ceiling((double)_bsdbContext.Article.Where(y => y.Name.Contains(dboArticle.search) && !y.Deleted).Count() / dboArticle.pageSize);
                }
            }
            else
            {
                dboArticle.Products = await _bsdbContext.Article.Include(s => s.SaleItem)
                                                 .Include(a => a.Category)
                                                 .Include(a => a.Brand)
                                                 .Include(p => p.Instructions.Where(i => !i.Deleted))
                                                 .Where(y => !y.Deleted)
                                                 .Skip(dboArticle.pageSize * (dboArticle.page - 1))
                                                 .Take(dboArticle.pageSize)
                                                 .ToListAsync();

                dboArticle.pageCount = (int)Math.Ceiling((double)_bsdbContext.Article.Where(y => !y.Deleted).Count() / dboArticle.pageSize);
            }

            return dboArticle;
        }

        public async Task<bool> AddArticle(Article article)
        {
            try
            {
                _bsdbContext.Article.Add(article);
                var result = await _bsdbContext.SaveChangesAsync();
                return (result > -1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateArticle(Article article)
        {
            try
            {
                if (_bsdbContext.Article.Any(a => a.Id == article.Id))
                {
                    _bsdbContext.Entry(article).State = EntityState.Modified;

                    foreach (Instruction instruction in article.Instructions)
                        if (_bsdbContext.Instruction.Any(a => a.Id == instruction.Id))
                            _bsdbContext.Entry(instruction).State = EntityState.Modified;
                        else
                            _bsdbContext.Entry(instruction).State = EntityState.Added;
                }

                var result = await _bsdbContext.SaveChangesAsync();
                return (result > -1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Article> Article(Guid articleId)
        {
            try
            {
                if (_bsdbContext.Article.Count() > 0)
                {
                    var articles = _bsdbContext.Article.Include(s => s.SaleItem).Include(p => p.Instructions).Where(x => x.Id == articleId).ToList();
                    if (articles.Count > 0)
                        return articles.FirstOrDefault();
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
