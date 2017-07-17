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
		//public Entity(string type)
        public Entity(EntityTypes type)
        {
            this.Type = type.ToString();
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
        [JsonProperty("type")]
        public string Type { get; private set; }
        
    }
}
