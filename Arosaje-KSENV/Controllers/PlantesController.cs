using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arosaje_KSENV.Models;

namespace Arosaje_KSENV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantesController : ControllerBase
    {
        private readonly ArosajeKsenvContext _context;

        public PlantesController(ArosajeKsenvContext context)
        {
            _context = context;
        }

        // GET: api/Plantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plante>>> GetPlantes()
        {
          if (_context.Plantes == null)
          {
              return NotFound();
          }
            return await _context.Plantes.ToListAsync();
        }

        // GET: api/Plantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plante>> GetPlante(int id)
        {
          if (_context.Plantes == null)
          {
              return NotFound();
          }
            var plante = await _context.Plantes.FindAsync(id);

            if (plante == null)
            {
                return NotFound();
            }

            return plante;
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Plante>> GetPlanteFromName(string name)
        {
            if (_context.Plantes == null)
            {
                return NotFound();
            }
            var plante = await _context.Plantes.FirstOrDefaultAsync(v => v.Nom == name);

            if (plante == null)
            {
                return NotFound();
            }

            return plante;
        }

        // PUT: api/Plantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlante(int id, Plante plante)
        {
            if (id != plante.IdPlante)
            {
                return BadRequest();
            }

            _context.Entry(plante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanteExists(id))
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

        // POST: api/Plantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plante>> PostPlante(Plante plante)
        {
          if (_context.Plantes == null)
          {
              return Problem("Entity set 'ArosajeKsenvContext.Plantes'  is null.");
          }
            _context.Plantes.Add(plante);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlante", new { id = plante.IdPlante }, plante);
        }

        // DELETE: api/Plantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlante(int id)
        {
            if (_context.Plantes == null)
            {
                return NotFound();
            }
            var plante = await _context.Plantes.FindAsync(id);
            if (plante == null)
            {
                return NotFound();
            }

            _context.Plantes.Remove(plante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanteExists(int id)
        {
            return (_context.Plantes?.Any(e => e.IdPlante == id)).GetValueOrDefault();
        }
    }
}
