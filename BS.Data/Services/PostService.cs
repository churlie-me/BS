using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using BS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class PostService : IPostService
    {
        public IPostRepository postRepository;
        public PostService(PostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task<bool> AddOrModify(Post post)
        {
            try
            {
                return await postRepository.AddOrModify(post);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Post> Post(Guid id)
        {
            try
            {
                return await postRepository.Post(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboPost> Posts(dboPost dboPost)
        {
            try
            {
                return await postRepository.Posts(dboPost);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
