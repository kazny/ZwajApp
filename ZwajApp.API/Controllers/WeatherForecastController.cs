using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZwajApp.API.Data;

namespace ZwajApp.API.Controllers
{
    

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        public DataContext _context { get; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
        DataContext context)
        {
            _context = context;
            _logger = logger;
        }
        // [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task< IActionResult> Get(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync( v => v.Id == id);
            if (value == null)
                return NotFound();
            return Ok(value);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task< IActionResult> Get()
        {
           return Ok( await _context.Values.ToListAsync());
        }
    }
}
