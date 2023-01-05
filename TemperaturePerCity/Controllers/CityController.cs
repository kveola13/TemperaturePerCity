using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemperaturePerCity.Models;

namespace TemperaturePerCity.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityDb _context;

        public CityController(CityDb context)
        {
            _context = context;
        }

        // GET: api/CityDTOes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCityDTO()
        {
            return await _context.CityDTO.ToListAsync();
        }

        // GET: api/CityDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> GetCityDTO(int id)
        {
            var cityDTO = await _context.CityDTO.FindAsync(id);

            if (cityDTO == null)
            {
                return NotFound();
            }

            return cityDTO;
        }

        // PUT: api/CityDTOes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCityDTO(int id, CityDTO cityDTO)
        {
            if (id != cityDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(cityDTO).State = EntityState.Modified;
            var cityItem = await _context.CityDTO.FindAsync(id);
            if (cityItem == null)
            {
                return NotFound();
            }
            cityItem.CityName= cityDTO.CityName;
            cityItem.Country= cityDTO.Country;
            cityItem.Continent= cityDTO.Continent;
            cityItem.CurrentTime= DateTime.Now;
            cityItem.Temperature= cityDTO.Temperature;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityDTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CityDTOes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CityDTO>> PostCityDTO(CityDTO cityDTO)
        {
            cityDTO.CurrentTime= DateTime.Now;
            _context.CityDTO.Add(cityDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCityDTO", new { id = cityDTO.Id }, cityDTO);
        }

        // DELETE: api/CityDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCityDTO(int id)
        {
            var cityDTO = await _context.CityDTO.FindAsync(id);
            if (cityDTO == null)
            {
                return NotFound();
            }

            _context.CityDTO.Remove(cityDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityDTOExists(int id)
        {
            return _context.CityDTO.Any(e => e.Id == id);
        }

        [HttpGet("populate")]
        public async void Init()
        {
            var firstCity = new CityDTO 
            { 
                CityName = "Oslo", 
                Continent = "Europe", 
                Country = "Norway", 
                CurrentTime= DateTime.Now, 
                Temperature=-6 
            };
            var secondCity = new CityDTO
            {
                CityName = "Osaka",
                Continent = "Asia",
                Country = "Japan",
                CurrentTime = DateTime.Now.AddHours(10),
                Temperature = 10
            };
            var thirdCity = new CityDTO
            {
                CityName = "Dakar",
                Continent = "Africa",
                Country = "Senegal",
                CurrentTime = DateTime.Now.AddHours(-1),
                Temperature = 26
            };
            await PostCityDTO(firstCity);
            await PostCityDTO(secondCity);
            await PostCityDTO(thirdCity);
        }

        [HttpGet("continent/{continent}")]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesOrder(string continent)
        {
            var list = _context.CityDTO.Where(c => c.Continent.ToLower().Equals(continent.ToLower()));
            if (!list.Any())
            {
                return NotFound($"Could not find cities in the {continent} continent");
            }
            return await list.ToListAsync();
        }

        [HttpGet("sort/reverse")]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesInReverseOrder()
        {
            var list = _context.CityDTO.Reverse().ToListAsync();
            return await list;
        }

        [HttpGet("sort/name")]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesAlphabetically()
        {
            return await _context.CityDTO.OrderBy(c=>c.CityName).ToListAsync();
        }

        [HttpGet("sort/id")]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesById()
        {
            var list = _context.CityDTO.OrderBy(c => c.Id).ToListAsync();
            return await list;
        }
        [HttpGet("sort/continent")]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesByContinent()
        {
            var list = _context.CityDTO.OrderBy(c=> c.Continent).ToListAsync();
            return await list;
        }

        [HttpGet("sort/temperature")]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesByTemperature()
        {
            var list = _context.CityDTO.OrderBy(c=> c.Temperature).ToListAsync();
            return await list;
        }

        [HttpGet("sort/time")]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitiesByTime()
        {
            var list = _context.CityDTO.OrderBy(c=> c.CurrentTime).ToListAsync();
            return await list;
        }
    }
}
