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
    public class TrechosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrechosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Trechoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trecho>>> GetTrechos()
        {
            return await _context.Trechos
                .Include(trecho => trecho.Hardware)
                .ToListAsync();

        }

        // GET: api/Trechoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trecho>> GetTrecho(Guid id)
        {
            var trecho = await _context.Trechos.FindAsync(id);

            if (trecho == null)
            {
                return NotFound();
            }

            return trecho;
        }

        // PUT: api/Trechoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrecho(Guid id, Trecho trecho)
        {
            if (id != trecho.Id)
            {
                return BadRequest();
            }

            _context.Entry(trecho).State = EntityState.Modified;
            _context.Entry(trecho).Property(nameof(Trecho.Codigo)).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrechoExists(id))
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

        // POST: api/Trechoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trecho>> PostTrecho(Trecho trecho)
        {
            _context.Trechos.Add(trecho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrecho", new { id = trecho.Id }, trecho);
        }

        // DELETE: api/Trechoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrecho(Guid id)
        {
            var trecho = await _context.Trechos.FindAsync(id);
            if (trecho == null)
            {
                return NotFound();
            }

            _context.Trechos.Remove(trecho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrechoExists(Guid id)
        {
            return _context.Trechos.Any(e => e.Id == id);
        }
    }
}
