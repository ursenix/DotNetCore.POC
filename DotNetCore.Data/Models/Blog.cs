using System;
using System.Collections.Generic;

namespace DotNetCore.Data.Models
{
    
	public class Blog
	{

        public Blog()
        {
            this.Posts = new List<Post>();
        }

        public int BlogId { get; set; }
		public string Url { get; set; }

		public ICollection<Post> Posts { get; set; }
		
    }

}
