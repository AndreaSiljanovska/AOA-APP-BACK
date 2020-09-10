using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOA.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AOA.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
    
        private readonly DataContext _context;

        public WeatherForecastController(DataContext context)
        {
            _context = context;

        }
        
        [HttpGet]
        public async Task<IActionResult> GetValues() //RETURNS HHTP REQUESTS
        {
          var values = await _context.Values.ToListAsync();
          return Ok(values);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }


    }
}
