using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arosaje_KSENV.Models;
using Microsoft.AspNetCore.Authorization;

namespace Arosaje_KSENV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly ArosajeKsenvContext _context;

        public UtilisateursController(ArosajeKsenvContext context)
        {
            _context = context;
        }

        // GET: api/Utilisateurs
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
          if (_context.Utilisateurs == null)
          {
              return NotFound();
          }
            return await _context.Utilisateurs.ToListAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
          if (_context.Utilisateurs == null)
          {
              return NotFound();
          }
            var utilisateur = await _context.Utilisateurs.FindAsync(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        [HttpGet("name/{mail}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurFromMail(string mail)
        {
            if (_context.Utilisateurs == null)
            {
                return NotFound();
            }
            var mail_replace = mail.Replace("-", " ");

            var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Email == mail_replace);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        [HttpGet("idByMail/{mail}")]
        public async Task<ActionResult<int>> GetUtilisateurIdFromMail(string mail)
        {
            if (_context.Utilisateurs == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs
                .Where(u => u.Email == mail)
                .Select(u => u.IdUtilisateur)
                .FirstOrDefaultAsync();

            if (utilisateur == 0)
            {
                return NotFound();
            }

            return Ok(utilisateur);
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.IdUtilisateur)
            {
                return BadRequest();
            }

            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
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

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
          if (_context.Utilisateurs == null)
          {
              return Problem("Entity set 'ArosajeKsenvContext.Utilisateurs'  is null.");
          }
            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.IdUtilisateur }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            if (_context.Utilisateurs == null)
            {
                return NotFound();
            }
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilisateurExists(int id)
        {
            return (_context.Utilisateurs?.Any(e => e.IdUtilisateur == id)).GetValueOrDefault();
        }
    }
}
