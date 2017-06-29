using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore.Service.ViewModels
{
    public class _Blog
    {
        public _Blog()
        {

        }
		public int BlogId { get; set; }
        [Required(ErrorMessage = "URL cannot be empty")]
		public string Url { get; set; }
    }
}
