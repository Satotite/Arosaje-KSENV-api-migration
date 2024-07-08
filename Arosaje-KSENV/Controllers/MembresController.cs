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
    public class MembresController : ControllerBase
    {
        private readonly ArosajeKsenvContext _context;

        public MembresController(ArosajeKsenvContext context)
        {
            _context = context;
        }

        // DTO for creating a member with only IdUtilisateur
        public class CreateMembreDto
        {
            public int IdUtilisateur { get; set; }
        }

        // GET: api/Membres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Membre>>> GetMembres()
        {
            if (_context.Membres == null)
            {
                return NotFound();
            }
            return await _context.Membres.ToListAsync();
        }

        // GET: api/Membres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Membre>> GetMembre(int id)
        {
            if (_context.Membres == null)
            {
                return NotFound();
            }
            var membre = await _context.Membres.FindAsync(id);

            if (membre == null)
            {
                return NotFound();
            }

            return membre;
        }

        // PUT: api/Membres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMembre(int id, Membre membre)
        {
            if (id != membre.IdUtilisateur)
            {
                return BadRequest();
            }

            _context.Entry(membre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembreExists(id))
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

        // POST: api/Membres
        [HttpPost]
        public async Task<ActionResult<Membre>> PostMembre(Membre membre)
        {
            if (_context.Membres == null)
            {
                return Problem("Entity set 'ArosajeKsenvContext.Membres' is null.");
            }
            _context.Membres.Add(membre);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MembreExists(membre.IdUtilisateur))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMembre", new { id = membre.IdUtilisateur }, membre);
        }

        // New endpoint: POST: api/Membres/CreateWithUserId
        [HttpPost("CreateWithUserId")]
        public async Task<ActionResult<Membre>> CreateMembreWithUserId(CreateMembreDto createMembreDto)
        {
            if (_context.Membres == null)
            {
                return Problem("Entity set 'ArosajeKsenvContext.Membres' is null.");
            }

            var membre = new Membre
            {
                IdUtilisateur = createMembreDto.IdUtilisateur
                // Initialize other properties if needed with default values
            };

            _context.Membres.Add(membre);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MembreExists(membre.IdUtilisateur))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMembre", new { id = membre.IdUtilisateur }, membre);
        }

        // DELETE: api/Membres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembre(int id)
        {
            if (_context.Membres == null)
            {
                return NotFound();
            }
            var membre = await _context.Membres.FindAsync(id);
            if (membre == null)
            {
                return NotFound();
            }

            _context.Membres.Remove(membre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MembreExists(int id)
        {
            return (_context.Membres?.Any(e => e.IdUtilisateur == id)).GetValueOrDefault();
        }
    }
}
