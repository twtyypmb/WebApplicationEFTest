using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationEFTest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {

        DbContext context = null;
        public ValueController(DbContext _context)
        {
            context = _context;
        }


        // GET: api/Value
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var user = new Entity.User
            {
                Id = Guid.NewGuid().ToString(),
                Age = 1.2,
                Name = "里斯",
                Sex = 1
            };

            var role = new Entity.Role()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "角色",
            };

            var user_role = new Entity.UserRole()
            {
                Id = Guid.NewGuid().ToString(),
                Role = role,
                User = user
            };

            context.Set<Entity.UserRole>().Add(user_role);
            var i = context.SaveChanges();

            return new string[] { "value1", "value2" };
        }

        // GET: api/Value/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Value
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Value/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
