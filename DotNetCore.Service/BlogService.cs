using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using DotNetCore.Data;
using DotNetCore.Data.Models;
using DotNetCore.Service.Contracts;
using DotNetCore.Service.ViewModels;
using DotNetCore.Settings;


namespace DotNetCore.Service
{
    public class BlogService : IBlogService
    {
        private readonly BlogContextUnitOfWork unitOfWork;
        private readonly ISettings settings;
        private readonly IMapper autoMapper;

        private IRepository<Blog> blogRepo;
        private IRepository<Post> postRepo;

        public BlogService(BlogContextUnitOfWork unitOfWork, ISettings settings, IMapper autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.settings = settings;
            this.autoMapper = autoMapper;

            ConstructRepos();
        }

        private void ConstructRepos()
        {
            this.blogRepo = unitOfWork.GetRepository<Blog>();
            this.postRepo = unitOfWork.GetRepository<Post>();
        }

        public _Blog AddBlog(_Blog blog)
        {
            var newBlog = autoMapper.Map<Blog>(blog);
            blogRepo.Insert(newBlog);
            return autoMapper.Map<_Blog>(newBlog);
        }

        public void UpdateBlog(int blogId, Blog blog)
        {
            var _blog = GetBlogById(blogId);

            _blog.Url = blog.Url;

            blogRepo.Update(_blog);
        }

        public IEnumerable<Blog> GetAllBlogs()
        {
            return blogRepo.Get();
        }

        public Blog GetBlogById(int id)
        {
            return blogRepo.GetById(id);
        }

        public IEnumerable<Blog> GetTopTenBlogs()
        {
            return blogRepo.Get().OrderByDescending(b => b.BlogId).Take(10).AsEnumerable();
        }

		public int GetBlogCount()
		{
            return blogRepo.Get().Count();
		}

        public Post AddPost_Alternate_NotWorking_Properly(int blogId, Post newPost)
        {
            //var blog = blogRepo.GetById(blogId);
            var blog = blogRepo.Get(b => b.BlogId == blogId).FirstOrDefault();

            if(blog != null)
            {
                //blog.Posts.Add(newPost);
                //unitOfWork.SaveChanges();

                newPost.BlogId = blogId;
                blog.Posts.Add(newPost);
                blogRepo.Update(blog);
            }

            return newPost;
        }

        public Post AddPost(int blogId, Post newPost)
        {
            newPost.BlogId = blogId;
            postRepo.Insert(newPost);
            return newPost;
        }

        public void UpdatePost(int blogId, int postId, Post postToUpdate)
        {
            postToUpdate.BlogId = blogId;
            postToUpdate.PostId = postId;
            postRepo.Update(postToUpdate);
        }

        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            return postRepo.Get(v => v.BlogId == blogId);
        }

        public Post GetBlogPost(int postId)
        {
            return postRepo.GetById(postId);
        }

        public void DeleteBlog(int blogId)
        {
            blogRepo.Delete(blogId);
        }
    }
}
