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
    public class DateTipsController : ControllerBase
    {
        private readonly ArosajeKsenvContext _context;

        public DateTipsController(ArosajeKsenvContext context)
        {
            _context = context;
        }

        // GET: api/DateTips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DateTip>>> GetDateTips()
        {
          if (_context.DateTips == null)
          {
              return NotFound();
          }
            return await _context.DateTips.ToListAsync();
        }

        // GET: api/DateTips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DateTip>> GetDateTip(int id)
        {
          if (_context.DateTips == null)
          {
              return NotFound();
          }
            var dateTip = await _context.DateTips.FindAsync(id);

            if (dateTip == null)
            {
                return NotFound();
            }

            return dateTip;
        }

        // PUT: api/DateTips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDateTip(int id, DateTip dateTip)
        {
            if (id != dateTip.IdTips)
            {
                return BadRequest();
            }

            _context.Entry(dateTip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DateTipExists(id))
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

        // POST: api/DateTips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DateTip>> PostDateTip(DateTip dateTip)
        {
          if (_context.DateTips == null)
          {
              return Problem("Entity set 'ArosajeKsenvContext.DateTips'  is null.");
          }
            _context.DateTips.Add(dateTip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDateTip", new { id = dateTip.IdTips }, dateTip);
        }

        // DELETE: api/DateTips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDateTip(int id)
        {
            if (_context.DateTips == null)
            {
                return NotFound();
            }
            var dateTip = await _context.DateTips.FindAsync(id);
            if (dateTip == null)
            {
                return NotFound();
            }

            _context.DateTips.Remove(dateTip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DateTipExists(int id)
        {
            return (_context.DateTips?.Any(e => e.IdTips == id)).GetValueOrDefault();
        }
    }
}
