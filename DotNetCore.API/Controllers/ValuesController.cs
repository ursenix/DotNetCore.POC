using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Data;
using DotNetCore.Data.Models;
using DotNetCore.Settings;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.API.Controllers
{
    [Route("api")]
    public class ValuesController : Controller
    {

        //BloggingContext _context;
        //ISettings _settings;
        //TestService testService;
        RepoManager repo;

        public ValuesController(RepoManager repo)
        {
            //this._context = blogContext;
            //this._settings = settings;
            //this.testService = new TestService(_settings);
            this.repo = repo;
        }
        // GET api/values
        [HttpGet("test")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", repo.GetBlogCount().ToString() };
            //return new string[] { "value1", "value2" };
        }

        [HttpGet("getthekey")]
        public IActionResult GetTheKey()
        {
            return Ok(repo.GetTheKey());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
