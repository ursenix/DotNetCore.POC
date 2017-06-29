using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCore.Data.Models;
using DotNetCore.Service.ViewModels;

namespace DotNetCore.Service.Contracts
{
    public interface IBlogService
    {
        Task<Result<_Blog>> AddBlogAsync(_Blog blog);
        void UpdateBlog(int blogId, Blog blog);
        IEnumerable<Blog> GetAllBlogs();
        Blog GetBlogById(int id);
        IEnumerable<Blog> GetTopTenBlogs();
        Post AddPost(int blogId, Post newPost);
        void UpdatePost(int blogId, int postId, Post postToUpdate);
        int GetBlogCount();
        IEnumerable<Post> GetBlogPosts(int blogId);
        Post GetBlogPost(int postId);
        void DeleteBlog(int blogId);
    }
}
