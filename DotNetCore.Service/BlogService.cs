using System;
using System.Linq;
using DotNetCore.Data;
using DotNetCore.Data.Models;
using DotNetCore.Service.Contracts;
using DotNetCore.Settings;

namespace DotNetCore.Service
{
    public class BlogService : IBlogService
    {
        private readonly BlogContextUnitOfWork unitOfWork;
        private readonly ISettings settings;

        private IRepository<Blog> blogRepo;
        private IRepository<Post> postRepo;

        public BlogService(BlogContextUnitOfWork unitOfWork, ISettings settings)
        {
            this.unitOfWork = unitOfWork;
            this.settings = settings;

            ConstructRepos();
        }

        private void ConstructRepos()
        {
            this.blogRepo = unitOfWork.GetRepository<Blog>();
            this.postRepo = unitOfWork.GetRepository<Post>();
        }

		public int GetBlogCount()
		{
            return blogRepo.Get().Count();
		}

		public string GetTheKey()
		{
			return settings.DocumentDBSetting.AuthKey;
		}
    }
}
