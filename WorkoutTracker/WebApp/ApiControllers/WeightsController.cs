using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class WeightsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WeightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Weights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weight>>> GetWeights()
        {
          if (_context.Weights == null)
          {
              return NotFound();
          }
            return await _context.Weights.ToListAsync();
        }

        // GET: api/Weights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Weight>> GetWeight(Guid id)
        {
          if (_context.Weights == null)
          {
              return NotFound();
          }
            var weight = await _context.Weights.FindAsync(id);

            if (weight == null)
            {
                return NotFound();
            }

            return weight;
        }

        // PUT: api/Weights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeight(Guid id, Weight weight)
        {
            if (id != weight.Id)
            {
                return BadRequest();
            }

            _context.Entry(weight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeightExists(id))
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

        // POST: api/Weights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Weight>> PostWeight(Weight weight)
        {
          if (_context.Weights == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Weights'  is null.");
          }
            _context.Weights.Add(weight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeight", new { id = weight.Id }, weight);
        }

        // DELETE: api/Weights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeight(Guid id)
        {
            if (_context.Weights == null)
            {
                return NotFound();
            }
            var weight = await _context.Weights.FindAsync(id);
            if (weight == null)
            {
                return NotFound();
            }

            _context.Weights.Remove(weight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeightExists(Guid id)
        {
            return (_context.Weights?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
*/