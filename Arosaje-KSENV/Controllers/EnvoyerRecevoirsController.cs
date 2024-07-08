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
    public class EnvoyerRecevoirsController : ControllerBase
    {
        private readonly ArosajeKsenvContext _context;

        public EnvoyerRecevoirsController(ArosajeKsenvContext context)
        {
            _context = context;
        }

        // GET: api/EnvoyerRecevoirs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnvoyerRecevoir>>> GetEnvoyerRecevoirs()
        {
          if (_context.EnvoyerRecevoirs == null)
          {
              return NotFound();
          }
            return await _context.EnvoyerRecevoirs.ToListAsync();
        }

        // GET: api/EnvoyerRecevoirs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnvoyerRecevoir>> GetEnvoyerRecevoir(int id)
        {
          if (_context.EnvoyerRecevoirs == null)
          {
              return NotFound();
          }
            var envoyerRecevoir = await _context.EnvoyerRecevoirs.FindAsync(id);

            if (envoyerRecevoir == null)
            {
                return NotFound();
            }

            return envoyerRecevoir;
        }

        // PUT: api/EnvoyerRecevoirs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnvoyerRecevoir(int id, EnvoyerRecevoir envoyerRecevoir)
        {
            if (id != envoyerRecevoir.IdUtilisateur)
            {
                return BadRequest();
            }

            _context.Entry(envoyerRecevoir).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnvoyerRecevoirExists(id))
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

        // POST: api/EnvoyerRecevoirs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EnvoyerRecevoir>> PostEnvoyerRecevoir(EnvoyerRecevoir envoyerRecevoir)
        {
          if (_context.EnvoyerRecevoirs == null)
          {
              return Problem("Entity set 'ArosajeKsenvContext.EnvoyerRecevoirs'  is null.");
          }
            _context.EnvoyerRecevoirs.Add(envoyerRecevoir);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EnvoyerRecevoirExists(envoyerRecevoir.IdUtilisateur))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEnvoyerRecevoir", new { id = envoyerRecevoir.IdUtilisateur }, envoyerRecevoir);
        }

        // DELETE: api/EnvoyerRecevoirs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvoyerRecevoir(int id)
        {
            if (_context.EnvoyerRecevoirs == null)
            {
                return NotFound();
            }
            var envoyerRecevoir = await _context.EnvoyerRecevoirs.FindAsync(id);
            if (envoyerRecevoir == null)
            {
                return NotFound();
            }

            _context.EnvoyerRecevoirs.Remove(envoyerRecevoir);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnvoyerRecevoirExists(int id)
        {
            return (_context.EnvoyerRecevoirs?.Any(e => e.IdUtilisateur == id)).GetValueOrDefault();
        }
    }
}
