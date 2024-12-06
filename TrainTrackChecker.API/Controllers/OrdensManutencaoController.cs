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
    public class OrdensManutencaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdensManutencaoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OrdensManutencao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemManutencao>>> GetOrdensManutencao()
        {
            return await _context.OrdensManutencao.ToListAsync();
        }

        // GET: api/OrdensManutencao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemManutencao>> GetOrdemManutencao(Guid id)
        {
            var ordemManutencao = await _context.OrdensManutencao.FindAsync(id);

            if (ordemManutencao == null)
            {
                return NotFound();
            }

            return ordemManutencao;
        }

        // PUT: api/OrdensManutencao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdemManutencao(Guid id, OrdemManutencao ordemManutencao)
        {
            if (id != ordemManutencao.Id)
            {
                return BadRequest();
            }

            _context.Entry(ordemManutencao).State = EntityState.Modified;
            _context.Entry(ordemManutencao).Property(nameof(OrdemManutencao.Codigo)).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdemManutencaoExists(id))
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

        // POST: api/OrdensManutencao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdemManutencao>> PostOrdemManutencao(OrdemManutencao ordemManutencao)
        {
            _context.OrdensManutencao.Add(ordemManutencao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdemManutencao", new { id = ordemManutencao.Id }, ordemManutencao);
        }

        // DELETE: api/OrdensManutencao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdemManutencao(Guid id)
        {
            var ordemManutencao = await _context.OrdensManutencao.FindAsync(id);
            if (ordemManutencao == null)
            {
                return NotFound();
            }

            _context.OrdensManutencao.Remove(ordemManutencao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdemManutencaoExists(Guid id)
        {
            return _context.OrdensManutencao.Any(e => e.Id == id);
        }
    }
}
