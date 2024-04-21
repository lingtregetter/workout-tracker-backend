using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Domain;
using App.Domain.Identity;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class PersonalInformationsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUnitOfWork _appUnitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="appUnitOfWork"></param>
        public PersonalInformationsController(UserManager<AppUser> userManager, IAppUnitOfWork appUnitOfWork)
        {
            _userManager = userManager;
            _appUnitOfWork = appUnitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: PersonalInformations
        public async Task<IActionResult> Index()
        {
            var viewModel = await _appUnitOfWork
                .PersonalInformationRepository.AllAsync(User.GetUserId());
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: PersonalInformations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInformation =
                await _appUnitOfWork.PersonalInformationRepository.FindAsync(id.Value, User.GetUserId());
            if (personalInformation == null)
            {
                return NotFound();
            }

            return View(personalInformation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: PersonalInformations/Create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personalInformation"></param>
        /// <returns></returns>
        // POST: PersonalInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonalInformation personalInformation)
        {
            personalInformation.AppUserId = User.GetUserId();

            if (ModelState.IsValid)
            {
                _appUnitOfWork.PersonalInformationRepository.Add(personalInformation);
                await _appUnitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(personalInformation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: PersonalInformations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInformation =
                await _appUnitOfWork.PersonalInformationRepository.FindAsync(id.Value, User.GetUserId());
            if (personalInformation == null)
            {
                return NotFound();
            }

            return View(personalInformation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personalInformation"></param>
        /// <returns></returns>
        // POST: PersonalInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PersonalInformation personalInformation)
        {
            // TODO: check the ownership before edit!!!! also before delete

            if (id != personalInformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && await _appUnitOfWork.PersonalInformationRepository
                    .IsOwnedByUserAsync(personalInformation.Id, User.GetUserId()))
            {
                personalInformation.AppUserId = User.GetUserId();

                _appUnitOfWork.PersonalInformationRepository.Update(personalInformation);
                await _appUnitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(personalInformation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: PersonalInformations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInformation =
                await _appUnitOfWork.PersonalInformationRepository.FindAsync(id.Value, User.GetUserId());
            if (personalInformation == null)
            {
                return NotFound();
            }

            return View(personalInformation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: PersonalInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _appUnitOfWork.PersonalInformationRepository.RemoveAsync(id, User.GetUserId());
            await _appUnitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}