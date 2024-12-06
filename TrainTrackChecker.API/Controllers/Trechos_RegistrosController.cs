using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainTrackChecker.API;
using TrainTrackChecker.API.Models;

namespace TrainTrackChecker.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class Trechos_RegistrosController : ControllerBase {
        private readonly AppDbContext _context;

        public Trechos_RegistrosController(AppDbContext context) {
            _context = context;
        }

        // GET: api/Trecho_Registro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trecho_Registro>>> GetTrechos_Registros() {
            return await _context.Trechos_Registros
                .Include(x => x.Trecho)
                .ToListAsync();
        }

        // GET: api/Trecho_Registro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trecho_Registro>> GetTrecho_Registro(Guid id) {
            var trecho_Registro = await _context.Trechos_Registros.FindAsync(id);

            if (trecho_Registro == null) {
                return NotFound();
            }

            return trecho_Registro;
        }

        // PUT: api/Trecho_Registro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrecho_Registro(Guid id, Trecho_Registro trecho_Registro) {
            if (id != trecho_Registro.Id) {
                return BadRequest();
            }

            _context.Entry(trecho_Registro).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!Trecho_RegistroExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trecho_Registro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trecho_Registro>> PostTrecho_Registro(Trecho_Registro trecho_Registro) {
            _context.Trechos_Registros.Add(trecho_Registro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrecho_Registro", new { id = trecho_Registro.Id }, trecho_Registro);
        }

        // DELETE: api/Trecho_Registro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrecho_Registro(Guid id) {
            var trecho_Registro = await _context.Trechos_Registros.FindAsync(id);
            if (trecho_Registro == null) {
                return NotFound();
            }

            _context.Trechos_Registros.Remove(trecho_Registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Trecho_RegistroExists(Guid id) {
            return _context.Trechos_Registros.Any(e => e.Id == id);
        }
    }
}