using System;
using Newtonsoft.Json;

namespace DotNetCore.Data.Models
{
	public class ContactAddress : Entity
	{
		public ContactAddress() : base("address")
		{
			Primary = false;
		}
		[JsonProperty("user")]
		public string User { get; set; }
		[JsonProperty("address")]
		public string Address { get; set; }
		[JsonProperty("primary")]
		public bool Primary { get; set; }
	}
}
