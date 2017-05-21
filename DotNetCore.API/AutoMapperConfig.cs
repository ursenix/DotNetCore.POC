using System;
using AutoMapper;
using DotNetCore.Data.Models;
using DotNetCore.Service.ViewModels;

namespace DotNetCore.API
{
	public class AutoMapperProfileConfiguration : Profile
	{
		public AutoMapperProfileConfiguration()
		: this("MyProfile")
		{
		}
		protected AutoMapperProfileConfiguration(string profileName)
		: base(profileName)
		{
            CreateMap<_Blog, Blog>().ReverseMap();
            CreateMap<_Post, Post>().ReverseMap();
		}
	}
}
