using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Settings;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace DotNetCore.Data
{
    public class DocumentDBProvider
    {
		private readonly string databaseName;
		private readonly string collectionName;
        private readonly Uri collectionUri;

		public DocumentDBProvider(ISettings settings)
		{
			this.client = new DocumentClient(settings.DocumentDBSettings.DatabaseUri, settings.DocumentDBSettings.DatabaseKey, new ConnectionPolicy()
			{
				MaxConnectionLimit = 100,
				//ConnectionMode = ConnectionMode.Direct,
				//ConnectionProtocol = Protocol.Tcp
				// https://github.com/Azure/azure-documentdb-dotnet/issues/194
			});

            client.OpenAsync();

			this.databaseName = settings.DocumentDBSettings.DatabaseName;
			this.collectionName = settings.DocumentDBSettings.CollectionName;

			this.CreateDatabaseIfNotExistsAsync().Wait();
			this.CreateCollectionIfNotExistsAsync().Wait();

            this.collectionUri = GetCollectionLink();
		}

		public readonly DocumentClient client;

		public async Task CreateDatabaseIfNotExistsAsync()
		{
			try
			{
				await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					await client.CreateDatabaseAsync(new Database { Id = databaseName });
				}
				else
				{
					throw;
				}
			}
		}

		public async Task CreateCollectionIfNotExistsAsync()
		{
			try
			{
				await client.ReadDocumentCollectionAsync(
					UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					await client.CreateDocumentCollectionAsync(
						UriFactory.CreateDatabaseUri(databaseName),
						new DocumentCollection { Id = collectionName },
						new RequestOptions { OfferThroughput = 1000 });
				}
				else
				{
					throw;
				}
			}
		}

		private Uri GetCollectionLink()
		{
			return UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);
		}

		private Uri GetDocumentLink(string id)
		{
			return UriFactory.CreateDocumentUri(databaseName, collectionName, id);
		}

		public async Task<string> AddItem<T>(T document)
		{
            var result = await client.CreateDocumentAsync(collectionUri, document);
			return result.Resource.Id;
		}

		public async Task<string> UpdateItem<T>(T document, string id)
		{
            var result = await client.ReplaceDocumentAsync(GetDocumentLink(id), document);
			return result.Resource.Id;
		}

		public async Task DeleteItem(string id)
		{
            await client.DeleteDocumentAsync(GetDocumentLink(id));
		}

		public IQueryable<T> CreateQuery<T>(FeedOptions feedOptions)
		{
            return client.CreateDocumentQuery<T>(collectionUri, feedOptions);
		}

		public IQueryable<T> CreateQuery<T>(string sqlExpression, FeedOptions feedOptions)
		{
            return client.CreateDocumentQuery<T>(collectionUri, sqlExpression, feedOptions);
		}


	}
}
