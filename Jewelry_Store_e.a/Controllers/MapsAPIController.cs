using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jewelry_Store_e.a.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jewelry_Store_e.a.Controllers
{
    [Route("api/maps")]
    [ApiController]
    public class MapsAPIController : BaseController
    {
        public MapsAPIController(SDMDbContext context) : base(context)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("address")]
        public async Task<ActionResult<List<Shipment>>> Address()
        {
            return await _context.Shipments.ToListAsync();
        }
    }
}