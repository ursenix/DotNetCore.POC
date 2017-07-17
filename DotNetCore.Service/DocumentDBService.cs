using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetCore.Data;
using DotNetCore.Data.Models;
using DotNetCore.Service.Contracts;
using DotNetCore.Service.Extensions;
using DotNetCore.Service.ViewModels;
using DotNetCore.Settings;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace DotNetCore.Service
{
    public class DocumentDBService : IDocumentDBService
    {
        private readonly ISettings settings;
        private readonly IMapper autoMapper;


        private readonly DocumentDBProvider provider;

        public DocumentDBService(ISettings settings, IMapper autoMapper, DocumentDBProvider provider)
        {
            this.settings = settings;
            this.autoMapper = autoMapper;
            this.provider = provider;
        }

        public async Task<_PagedResults<ContactAddress>> GetContactAddresses(int size = 10, string continuationToken = "")
		{
			var feedOptions = new FeedOptions() { MaxItemCount = size };
			if (!string.IsNullOrEmpty(continuationToken))
			{
				feedOptions.RequestContinuation = continuationToken;
			}
            return await provider.CreateQuery<ContactAddress>(feedOptions).Where(x => x.Type == EntityTypes.Address.ToString()).ToPagedResults();
        }

		public async Task<string> AddContactAddress(ContactAddress address)
		{
			return await provider.AddItem<ContactAddress>(address);
		}

		/// <summary>
		/// Updates a contact address
		/// </summary>
		/// <param name="address"></param>
		public async Task UpdateContactAddress(ContactAddress address)
		{
			await provider.UpdateItem<ContactAddress>(address, address.Id);
		}

		/// <summary>
		/// Deletes a contact address
		/// </summary>
		/// <param name="address"></param>
		public async Task DeleteContactAddress(string id)
		{
			await provider.DeleteItem(id);
		}

    }
}

