using System;
namespace DotNetCore.Service.Contracts
{
    public interface IBlogService
    {
        int GetBlogCount();
        string GetTheKey();
    }
}
