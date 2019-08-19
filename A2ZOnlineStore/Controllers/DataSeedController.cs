using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2ZOnlineStore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace A2ZOnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSeedController : ControllerBase
    {
        private readonly IServiceProvider service;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeedController(IServiceProvider service, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.service = service;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet,Route("SeedData")]
        public IActionResult Seed()
        {
            try
            {
                AppDbContext context = (AppDbContext)service.GetService(typeof(AppDbContext));
                DbInitializer.Seed(context, _userManager, _roleManager);
                return Ok("Data Seeded Successfuly");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}