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
    [Route("api/[controller]")]
    [ApiController]
    public class CityDTOesController : ControllerBase
    {
        private readonly CityDb _context;

        public CityDTOesController(CityDb context)
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
    }
}
