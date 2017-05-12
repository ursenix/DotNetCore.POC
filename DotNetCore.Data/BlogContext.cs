using System;
using DotNetCore.Settings;
using Microsoft.EntityFrameworkCore;
using DotNetCore.Data.Models;

namespace DotNetCore.Data
{
	public class BloggingContext : DbContext
	{
		public BloggingContext(DbContextOptions<BloggingContext> options)
			: base(options)
		{
            
        }



        //readonly ISettings _settings;

        //public BloggingContext(ISettings settings)
        //{
        //    this._settings = settings;
        //}


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Has to get it from appSettings
        //    //optionsBuilder.UseSqlServer(@"Server=tcp:officemanager.database.windows.net,1433;Initial Catalog=OfficeManager;Persist Security Info=False;User ID=officemanageradmin;Password=p@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        //    optionsBuilder.UseSqlServer(_settings.OfficeManagerConnectionString);
        //}


		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }
	}
}
