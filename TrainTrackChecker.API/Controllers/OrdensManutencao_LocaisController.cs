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
    public class OrdensManutencao_LocaisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdensManutencao_LocaisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OrdensManutencao_Locais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemManutencao_Local>>> GetOrdensManutencao_Locais()
        {
            return await _context.OrdensManutencao_Locais
                .Include(x => x.OrdemManutencao)
                .ToListAsync();
        }

        // GET: api/OrdensManutencao_Locais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemManutencao_Local>> GetOrdemManutencao_Local(Guid id)
        {
            var ordemManutencao_Local = await _context.OrdensManutencao_Locais.FindAsync(id);

            if (ordemManutencao_Local == null)
            {
                return NotFound();
            }

            return ordemManutencao_Local;
        }

        // PUT: api/OrdensManutencao_Locais/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdemManutencao_Local(Guid id, OrdemManutencao_Local ordemManutencao_Local)
        {
            if (id != ordemManutencao_Local.Id)
            {
                return BadRequest();
            }

            _context.Entry(ordemManutencao_Local).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdemManutencao_LocalExists(id))
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

        // POST: api/OrdensManutencao_Locais
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdemManutencao_Local>> PostOrdemManutencao_Local(OrdemManutencao_Local ordemManutencao_Local)
        {
            _context.OrdensManutencao_Locais.Add(ordemManutencao_Local);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdemManutencao_Local", new { id = ordemManutencao_Local.Id }, ordemManutencao_Local);
        }

        // DELETE: api/OrdensManutencao_Locais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdemManutencao_Local(Guid id)
        {
            var ordemManutencao_Local = await _context.OrdensManutencao_Locais.FindAsync(id);
            if (ordemManutencao_Local == null)
            {
                return NotFound();
            }

            _context.OrdensManutencao_Locais.Remove(ordemManutencao_Local);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdemManutencao_LocalExists(Guid id)
        {
            return _context.OrdensManutencao_Locais.Any(e => e.Id == id);
        }
    }
}
