using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Data;
using DotNetCore.Data.Models;
using DotNetCore.Service.Contracts;
using DotNetCore.Service.ViewModels;
using DotNetCore.Settings;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.API.Controllers
{
    [Route("api")]
    public class BlogsController : Controller
    {
        IBlogService blogService;

        public BlogsController(IBlogService blogService)
        {
            this.blogService = blogService;
        }
        // GET api/blogs
        [HttpGet("blogs")]
        public IActionResult Get()
        {
            return Ok(blogService.GetAllBlogs());
        }

        // GET api/blogs/5
        [HttpGet("blogs/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(blogService.GetBlogById(id));
        }

        // POST api/blogs
        [HttpPost("blogs")]
        public IActionResult Post([FromBody]_Blog blog)
        {
            var newBlog = blogService.AddBlog(blog);
            return Created($"/api/blogs/{newBlog.BlogId}", newBlog);
        }

        // PUT api/blogs/5
        [HttpPut("blogs/{id}")]
        public void Put(int id, [FromBody]Blog blog)
        {
            blogService.UpdateBlog(id, blog);
        }

        // DELETE api/values/5
        [HttpDelete("blogs/{id}")]
        public void Delete(int id)
        {
            blogService.DeleteBlog(id);
        }

        // POST api/blogs/5/posts
        [HttpPost("blogs/{blogId}/posts")]
        public IActionResult AddPost(int blogId, [FromBody]Post post)
        {
            var newPost = blogService.AddPost(blogId, post);
            return Created($"/api/blogs/{blogId}/posts/{newPost.PostId}", newPost);
        }

        // PUT api/blogs/5/posts/8
        [HttpPut("blogs/{blogId}/posts/{postId}")]
        public IActionResult UpdatePost(int blogId, int postId, [FromBody]Post postToUpdate)
        {
            blogService.UpdatePost(blogId, postId, postToUpdate);

            return NoContent();
        }

		// GET api/blogs/5/posts
		[HttpGet("blogs/{blogId}/posts")]
		public IActionResult GetBlogPosts(int blogId)
		{
            return Ok(blogService.GetBlogPosts(blogId));
		}

        // GET api/blogs/5/posts/8
        [HttpGet("blogs/{blogId}/posts/{postId}")]
        public IActionResult GetBlogPostById(int blogId, int postId)
        {
            return Ok(blogService.GetBlogPost(postId));
        }
    }
}
