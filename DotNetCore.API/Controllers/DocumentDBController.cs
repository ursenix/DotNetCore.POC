using System;
using System.Threading.Tasks;
using DotNetCore.Data.Models;
using DotNetCore.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.API.Controllers
{
    [Route("api")]
    public class DocumentDBController : Controller
    {
        private readonly IDocumentDBService dbService;


        public DocumentDBController(IDocumentDBService dbService)
        {
            this.dbService = dbService;
        }

        [HttpGet("getContacts")]
        public async Task<IActionResult> GetContacts(string continuationToken="")
        {
            var results = await dbService.GetContactAddresses(10, continuationToken);

            return Json(results);
        }

		[HttpPost("addAddress")]
		public async Task<IActionResult> AddContactAddresses([FromBody] ContactAddress address)
		{
			if (string.IsNullOrEmpty(address.Id))
			{
				address.Id = await dbService.AddContactAddress(address);
			}
			else
			{
                await dbService.UpdateContactAddress(address);
			}
			return Json(address);
		}

		[HttpPut("editAddress")]
		public async Task<IActionResult> EditContactAddresses([FromBody] ContactAddress address)
		{
			if (string.IsNullOrEmpty(address.Id))
			{
                return BadRequest();
			}
			
			await dbService.UpdateContactAddress(address);
			
			return Json(address);
		}


		[HttpDelete("deleteAddress")]
		public async Task<IActionResult> RemoveContactAddresses(string id)
		{
            await dbService.DeleteContactAddress(id);
			return Ok();
		}

	}
}
