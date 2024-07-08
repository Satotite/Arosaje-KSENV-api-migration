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
    public class BotanistesController : ControllerBase
    {
        private readonly ArosajeKsenvContext _context;

        public BotanistesController(ArosajeKsenvContext context)
        {
            _context = context;
        }

        // GET: api/Botanistes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Botaniste>>> GetBotanistes()
        {
          if (_context.Botanistes == null)
          {
              return NotFound();
          }
            return await _context.Botanistes.ToListAsync();
        }

        // GET: api/Botanistes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Botaniste>> GetBotaniste(int id)
        {
          if (_context.Botanistes == null)
          {
              return NotFound();
          }
            var botaniste = await _context.Botanistes.FindAsync(id);

            if (botaniste == null)
            {
                return NotFound();
            }

            return botaniste;
        }

        // PUT: api/Botanistes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBotaniste(int id, Botaniste botaniste)
        {
            if (id != botaniste.IdUtilisateur)
            {
                return BadRequest();
            }

            _context.Entry(botaniste).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BotanisteExists(id))
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

        // POST: api/Botanistes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Botaniste>> PostBotaniste(Botaniste botaniste)
        {
          if (_context.Botanistes == null)
          {
              return Problem("Entity set 'ArosajeKsenvContext.Botanistes'  is null.");
          }
            _context.Botanistes.Add(botaniste);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BotanisteExists(botaniste.IdUtilisateur))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBotaniste", new { id = botaniste.IdUtilisateur }, botaniste);
        }

        // DELETE: api/Botanistes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBotaniste(int id)
        {
            if (_context.Botanistes == null)
            {
                return NotFound();
            }
            var botaniste = await _context.Botanistes.FindAsync(id);
            if (botaniste == null)
            {
                return NotFound();
            }

            _context.Botanistes.Remove(botaniste);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BotanisteExists(int id)
        {
            return (_context.Botanistes?.Any(e => e.IdUtilisateur == id)).GetValueOrDefault();
        }
    }
}
