using System;
using DotNetCore.Data.Models;
using System.Collections.Generic;
using System.Linq;
using DotNetCore.Settings;

namespace DotNetCore.Data
{
    public class RepoManager
    {
        readonly BloggingContext context;
        readonly ISettings _settings;

        public RepoManager(BloggingContext context, ISettings settings)
        {
            this.context = context;
            this._settings = settings;
        }

        public string GetBlogCount()
        {
            return context.Blogs.Count().ToString();
        }

		public string GetTheKey()
		{
			return _settings.DocumentDBSetting.AuthKey;
		}
    }
}
