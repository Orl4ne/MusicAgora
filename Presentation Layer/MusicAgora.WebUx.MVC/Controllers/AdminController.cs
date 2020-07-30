using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Identity.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.TransferObjects;
using MusicAgora.WebUx.MVC.Models;

namespace MusicAgora.WebUx.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILibraryUnitOfWork _libraryUnitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<AccessRight> _roleManager;

        public AdminController(ILogger<HomeController> logger,
            ILibraryUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            //IdentityRole identityRole,
            RoleManager<AccessRight> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _libraryUnitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #region Users Actions
            #region UsersIndex
            [HttpGet]
            public IActionResult UsersIndex()
            {
                var identityUsers = _userManager.Users.ToList();
                var libraryUsers = _libraryUnitOfWork.LibUserRepository.GetAll();
                var globalUsers = new List<GlobalUserVM>();
                foreach (var identityUser in identityUsers)
                {
                    var globalUserVM = new GlobalUserVM
                    {
                        IdentityUser = identityUser,
                        LibraryUser = libraryUsers.First(x => x.IdentityUserId == identityUser.Id),
                        Roles = _userManager.GetRolesAsync(identityUser).Result.ToList(),
                    };
                    globalUsers.Add(globalUserVM);
                }
                return View(globalUsers);
            }
            #endregion
        #endregion

    #region Instrument Actions
            #region InstrumentsIndex
            [HttpGet]
            public IActionResult InstrumentsIndex()
            {
                var instruments = _libraryUnitOfWork.InstrumentRepository.GetAll();
                return View(instruments);
            }
        #endregion
            #region CreateInstrument
            [HttpGet]
            public ActionResult CreateInstrument()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult CreateInstrument(InstrumentTO instrument)
            {
                try
                {
                    _libraryUnitOfWork.InstrumentRepository.Add(instrument);
                    return RedirectToAction(nameof(InstrumentsIndex));
                }
                catch
                {
                    return View();
                }
            }
        #endregion
            #region DeleteInstrument
            [HttpGet]
            public ActionResult DeleteInstrument(int id)
            {
                var instrument = _libraryUnitOfWork.InstrumentRepository.GetById(id);
                return View(instrument);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteInstrument(int id, InstrumentTO instrument)
            {
                try
                {
                    _libraryUnitOfWork.InstrumentRepository.Delete(instrument);
                    return RedirectToAction(nameof(InstrumentsIndex));
                }
                catch
                {
                    return View();
                }
            }
        #endregion
    #endregion
    }
}
