using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DotNetCore.Data.Models
{
	/// <summary>
	/// Type pattern for document storing
	/// </summary>
	public abstract class Entity
	{
		public Entity(string type)
		{
			this.Type = type;
		}
		/// <summary>
		/// Object unique identifier
		/// </summary>
		[Key]
		[JsonProperty("id")]
		public string Id { get; set; }
		/// <summary>
		/// Object type
		/// </summary>
		public string Type { get; private set; }
	}
}
