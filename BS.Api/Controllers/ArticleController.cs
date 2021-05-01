using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BS.Core;
using BS.Data;
using BS.Data.IServices;
using BS.Data.Repositories;
using BS.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(BSDBContext _bsdbContext)
        {
            _articleService = new ArticleService(new ArticleRepository(_bsdbContext));
        }

        // GET: api/Article/5
        [HttpPost("store")]
        public async Task<dboArticle> Get(dboArticle dboArticle)
        {
            try
            {
                return await _articleService.Articles(dboArticle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("detail/{productId}")]
        public async Task<Article> GetProduct(string productId)
        {
            try
            {
                return await _articleService.Article(Guid.Parse(productId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Article article)
        {
            try
            {
                var _article = await _articleService.Article(article.Id);
                if (_article == null)
                {
                    var result = await _articleService.AddArticle(article);
                    if (result)
                    {
                        return new HttpResponseMessage(HttpStatusCode.Created);
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotModified);
                    }
                }
                else
                {
                    var result = await _articleService.UpdateArticle(article);
                    if (result)
                    {
                        return new HttpResponseMessage(HttpStatusCode.Accepted);
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotModified);
                    }
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Article/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
