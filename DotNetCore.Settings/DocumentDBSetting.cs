using System;
using Microsoft.Extensions.Configuration;

namespace DotNetCore.Settings
{
	public class DocumentDBSettings
	{
		public DocumentDBSettings(IConfiguration configuration)
		{
			try
			{
				DatabaseName = configuration.GetSection("DocumentDBSettings:DatabaseName").Value;
				CollectionName = configuration.GetSection("DocumentDBSettings:CollectionName").Value;
				DatabaseUri = new Uri(configuration.GetSection("DocumentDBSettings:EndpointUri").Value);
				DatabaseKey = configuration.GetSection("DocumentDBSettings:Key").Value;
			}
			catch
			{
				throw new MissingFieldException("IConfiguration missing a valid Azure DocumentDB fields on DocumentDB > [DatabaseName,CollectionName,EndpointUri,Key]");
			}
		}

		public string DatabaseName { get; private set; }
		public string CollectionName { get; private set; }
		public Uri DatabaseUri { get; private set; }
		public string DatabaseKey { get; private set; }
	}
}
