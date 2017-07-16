﻿using System;
using System.Collections.Generic;

namespace DotNetCore.Service.ViewModels
{
	/// <summary>
	/// Paged results with continuation token
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class _PagedResults<T>
	{
		public _PagedResults()
		{
			Results = new List<T>();
		}
		/// <summary>
		/// Continuation Token for DocumentDB
		/// </summary>
		public string ContinuationToken { get; set; }

		/// <summary>
		/// Results
		/// </summary>
		public List<T> Results { get; set; }
	}
}
