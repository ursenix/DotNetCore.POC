using System;
using System.Collections.Generic;
using System.Net;

namespace DotNetCore.Service
{
    /// <summary>
    /// Pass ViewModel as T
    /// </summary>
	public class Result<T> where T : class
	{
		public Result()
		{
			EntityList = new List<T>();
			IsExecutionSucceed = false;
			//User has to set true explicitly on success
		}

		public T Entity { get; set; }
		public List<T> EntityList { get; set; }
		public object ObjectContainer { get; set; } //For common object
        public Exception Expection { get; set; }
		public bool IsExecutionSucceed { get; set; }
		public object PrimaryKey { get; set; } //Primary key value for the model
		public HttpStatusCode? StatusCode { get; set; }
		public string Message { get; set; }
	}
}
