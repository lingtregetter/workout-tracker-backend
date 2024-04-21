using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MuscleGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MuscleGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: MuscleGroups
        public async Task<IActionResult> Index()
        {
              return _context.MuscleGroups != null ? 
                          View(await _context.MuscleGroups.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MuscleGroups'  is null.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: MuscleGroups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.MuscleGroups == null)
            {
                return NotFound();
            }

            var muscleGroup = await _context.MuscleGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muscleGroup == null)
            {
                return NotFound();
            }

            return View(muscleGroup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: MuscleGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="muscleGroup"></param>
        /// <returns></returns>
        // POST: MuscleGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MuscleName,Id")] MuscleGroup muscleGroup)
        {
            if (ModelState.IsValid)
            {
                muscleGroup.Id = Guid.NewGuid();
                _context.Add(muscleGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(muscleGroup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: MuscleGroups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.MuscleGroups == null)
            {
                return NotFound();
            }

            var muscleGroup = await _context.MuscleGroups.FindAsync(id);
            if (muscleGroup == null)
            {
                return NotFound();
            }
            return View(muscleGroup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="muscleGroup"></param>
        /// <returns></returns>
        // POST: MuscleGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MuscleName,Id")] MuscleGroup muscleGroup)
        {
            if (id != muscleGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(muscleGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuscleGroupExists(muscleGroup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(muscleGroup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: MuscleGroups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.MuscleGroups == null)
            {
                return NotFound();
            }

            var muscleGroup = await _context.MuscleGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muscleGroup == null)
            {
                return NotFound();
            }

            return View(muscleGroup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: MuscleGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.MuscleGroups == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MuscleGroups'  is null.");
            }
            var muscleGroup = await _context.MuscleGroups.FindAsync(id);
            if (muscleGroup != null)
            {
                _context.MuscleGroups.Remove(muscleGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuscleGroupExists(Guid id)
        {
          return (_context.MuscleGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
