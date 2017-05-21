using System;
namespace DotNetCore.Service.ViewModels
{
    public class _Post
    {
        public _Post()
        {

        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
    }
}
