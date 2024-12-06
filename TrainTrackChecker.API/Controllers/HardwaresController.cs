using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainTrackChecker.API;
using TrainTrackChecker.API.Models;

namespace TrainTrackChecker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HardwaresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HardwaresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Hardwares
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hardware>>> GetHardwares()
        {
            return await _context.Hardwares.ToListAsync();
        }

        // GET: api/Hardwares/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hardware>> GetHardware(Guid id)
        {
            var hardware = await _context.Hardwares.FindAsync(id);

            if (hardware == null)
            {
                return NotFound();
            }

            return hardware;
        }

        // PUT: api/Hardwares/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHardware(Guid id, Hardware hardware)
        {
            if (id != hardware.Id)
            {
                return BadRequest();
            }

            _context.Entry(hardware).State = EntityState.Modified;
            _context.Entry(hardware).Property(nameof(Hardware.Codigo)).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HardwareExists(id))
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

        // POST: api/Hardwares
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hardware>> PostHardware(Hardware hardware)
        {
            _context.Hardwares.Add(hardware);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHardware", new { id = hardware.Id }, hardware);
        }

        // DELETE: api/Hardwares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHardware(Guid id)
        {
            var hardware = await _context.Hardwares.FindAsync(id);
            if (hardware == null)
            {
                return NotFound();
            }

            _context.Hardwares.Remove(hardware);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HardwareExists(Guid id)
        {
            return _context.Hardwares.Any(e => e.Id == id);
        }
    }
}
