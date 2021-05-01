using BS.Core;
using BS.Data;
using BS.Data.IServices;
using BS.Data.Repositories;
using BS.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(BSDBContext _bsdbContext)
        {
            _postService = new PostService(new PostRepository(_bsdbContext));
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Post post)
        {
            return await _postService.AddOrModify(post);
        }

        [HttpPost("listing")]
        public async Task<dboPost> Get(dboPost dboPost)
        {
            try
            {
                return await _postService.Posts(dboPost);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<Post> Get(Guid id)
        {
            return await _postService.Post(id);
        }
    }
}
