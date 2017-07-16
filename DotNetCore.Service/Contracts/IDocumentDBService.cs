using System;
using System.Threading.Tasks;
using DotNetCore.Data.Models;
using DotNetCore.Service.ViewModels;

namespace DotNetCore.Service.Contracts
{
    public interface IDocumentDBService
    {
        Task<_PagedResults<ContactAddress>> GetContactAddresses(int size, string continuationToken);
        Task<string> AddContactAddress(ContactAddress address);
        Task UpdateContactAddress(ContactAddress address);
        Task DeleteContactAddress(string id);
    }
}
