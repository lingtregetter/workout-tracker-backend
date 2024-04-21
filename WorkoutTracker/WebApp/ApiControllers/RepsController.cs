using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
/*
namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RepsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rep>>> GetReps()
        {
          if (_context.Reps == null)
          {
              return NotFound();
          }
            return await _context.Reps.ToListAsync();
        }

        // GET: api/Reps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rep>> GetRep(Guid id)
        {
          if (_context.Reps == null)
          {
              return NotFound();
          }
            var rep = await _context.Reps.FindAsync(id);

            if (rep == null)
            {
                return NotFound();
            }

            return rep;
        }

        // PUT: api/Reps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRep(Guid id, Rep rep)
        {
            if (id != rep.Id)
            {
                return BadRequest();
            }

            _context.Entry(rep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepExists(id))
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

        // POST: api/Reps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rep>> PostRep(Rep rep)
        {
          if (_context.Reps == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Reps'  is null.");
          }
            _context.Reps.Add(rep);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRep", new { id = rep.Id }, rep);
        }

        // DELETE: api/Reps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRep(Guid id)
        {
            if (_context.Reps == null)
            {
                return NotFound();
            }
            var rep = await _context.Reps.FindAsync(id);
            if (rep == null)
            {
                return NotFound();
            }

            _context.Reps.Remove(rep);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RepExists(Guid id)
        {
            return (_context.Reps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
*/