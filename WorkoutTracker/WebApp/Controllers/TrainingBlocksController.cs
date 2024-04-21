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
    public class TrainingBlocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public TrainingBlocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: TrainingBlocks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TrainingBlocks.Include(t => t.TrainingProgram);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: TrainingBlocks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.TrainingBlocks == null)
            {
                return NotFound();
            }

            var trainingBlock = await _context.TrainingBlocks
                .Include(t => t.TrainingProgram)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingBlock == null)
            {
                return NotFound();
            }

            return View(trainingBlock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: TrainingBlocks/Create
        public IActionResult Create()
        {
            ViewData["TrainingProgramId"] = new SelectList(_context.TrainingPrograms, "Id", "ProgramName");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainingBlock"></param>
        /// <returns></returns>
        // POST: TrainingBlocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlockName,TrainingProgramId,Id")] TrainingBlock trainingBlock)
        {
            if (ModelState.IsValid)
            {
                trainingBlock.Id = Guid.NewGuid();
                _context.Add(trainingBlock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainingProgramId"] = new SelectList(_context.TrainingPrograms, "Id", "ProgramName", trainingBlock.TrainingProgramId);
            return View(trainingBlock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: TrainingBlocks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.TrainingBlocks == null)
            {
                return NotFound();
            }

            var trainingBlock = await _context.TrainingBlocks.FindAsync(id);
            if (trainingBlock == null)
            {
                return NotFound();
            }
            ViewData["TrainingProgramId"] = new SelectList(_context.TrainingPrograms, "Id", "ProgramName", trainingBlock.TrainingProgramId);
            return View(trainingBlock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainingBlock"></param>
        /// <returns></returns>
        // POST: TrainingBlocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BlockName,TrainingProgramId,Id")] TrainingBlock trainingBlock)
        {
            if (id != trainingBlock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingBlock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingBlockExists(trainingBlock.Id))
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
            ViewData["TrainingProgramId"] = new SelectList(_context.TrainingPrograms, "Id", "ProgramName", trainingBlock.TrainingProgramId);
            return View(trainingBlock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: TrainingBlocks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.TrainingBlocks == null)
            {
                return NotFound();
            }

            var trainingBlock = await _context.TrainingBlocks
                .Include(t => t.TrainingProgram)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingBlock == null)
            {
                return NotFound();
            }

            return View(trainingBlock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: TrainingBlocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.TrainingBlocks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TrainingBlocks'  is null.");
            }
            var trainingBlock = await _context.TrainingBlocks.FindAsync(id);
            if (trainingBlock != null)
            {
                _context.TrainingBlocks.Remove(trainingBlock);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingBlockExists(Guid id)
        {
          return (_context.TrainingBlocks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
