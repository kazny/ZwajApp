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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        public DataContext _context { get; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
        DataContext context)
        {
            _context = context;
            _logger = logger;
        }
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
            /* var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Name = "khalid"
            })
            .ToArray(); */
           
        }
    }
}
