using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationEFTest.Entity;

namespace WebApplicationEFTest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        DbContext context = null;
        public DefaultController(DbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var user = new Entity.User
            {
                Age = 2.3,
                Id = Guid.NewGuid().ToString(),
                Name = "张三",
                Sex = 1
            };
            context.Set<Entity.User>().Add(user);
            if (context.SaveChanges() > 0)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("hehe");
            }
        }
    }
}