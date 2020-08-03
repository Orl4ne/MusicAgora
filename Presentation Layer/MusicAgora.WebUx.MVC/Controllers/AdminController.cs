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

        [HttpGet]
        public IActionResult UsersIndex()
        {
            var identityUsers = _userManager.Users.OrderBy(x => x.LastName).ToList();
            var libraryUsers = _libraryUnitOfWork.LibUserRepository.GetAll();
            foreach (var libUser in libraryUsers)
            {
                libUser.Instruments = new List<InstrumentTO>();
                foreach (var instruId in libUser.InstrumentIds)
                {
                    var instru = _libraryUnitOfWork.InstrumentRepository.GetById(instruId);
                    libUser.Instruments.Add(instru);
                }
            }
            var globalUsers = new List<GlobalUserVM>();
            foreach (var identityUser in identityUsers)
            {
                var globalUserVM = new GlobalUserVM
                {
                    IdentityUser = identityUser,
                    LibraryUser = libraryUsers.First(x => x.IdentityUserId == identityUser.Id),
                    UserRoles = _userManager.GetRolesAsync(identityUser).Result.ToList(),
                };
                globalUsers.Add(globalUserVM);
            }
            return View(globalUsers);
        }

        [HttpGet]
        public IActionResult ModifyUser(int id)
        {
            var identityUser = _userManager.FindByIdAsync(id.ToString()).Result;
            var libraryUsers = _libraryUnitOfWork.LibUserRepository.GetAll();
            var libUser = libraryUsers.First(x => x.IdentityUserId == identityUser.Id);
            libUser.Instruments = new List<InstrumentTO>();
            foreach (var instruId in libUser.InstrumentIds)
            {
                var instru = _libraryUnitOfWork.InstrumentRepository.GetById(instruId);
                libUser.Instruments.Add(instru);
            }


            var allInstruments = _libraryUnitOfWork.InstrumentRepository.GetAll().OrderBy(x=>x.Name).ToList();
            foreach (var instr in allInstruments)
            {
                if (libUser.Instruments.Any(inst=>inst.Id == instr.Id))
                {
                    instr.IsSelected = true;
                }
            }
            var roles = _roleManager.Roles.ToList();
            var globalUser = new GlobalUserVM
            {
                IdentityUser = identityUser,
                LibraryUser = libUser,
                UserRoles = _userManager.GetRolesAsync(identityUser).Result.ToList(),
                Roles = roles,
                AllInstruments = allInstruments,
            };
            return View(globalUser);
        }

        [HttpPost]
        public IActionResult ModifyUser(int id, GlobalUserVM globalUser)
        {
            try
            {
                var identityUser = _userManager.FindByIdAsync(id.ToString()).Result;
                identityUser.Email = globalUser.IdentityUser.Email;
                identityUser.FirstName = globalUser.IdentityUser.FirstName;
                identityUser.LastName = globalUser.IdentityUser.LastName;
                identityUser.IsGarde = globalUser.IdentityUser.IsGarde;
                identityUser.IsIndependance = globalUser.IdentityUser.IsIndependance;

                // We need to get all roles from DB, don't trust anything from the client:
                var roles = _roleManager.Roles.ToList();
                var libUser = _libraryUnitOfWork.LibUserRepository.GetByIdentityUserId(id);
                //libUser.Instruments = globalUser.LibraryUser.Instruments;
                
                libUser.Instruments = globalUser.AllInstruments.Where(inst => inst.IsSelected == true).ToList();
                libUser.InstrumentIds = libUser.Instruments.Select(instru => instru.Id).ToList();

                // Browse the roles to set or unset from SelectedRoles in View Model:
                var selectedRoles = globalUser.SelectedRoles;
                for (var i = 0; i < selectedRoles.Length; i++)
                {
                    // First: check if user has the current role in the loop:
                    var hasRole = _userManager.IsInRoleAsync(identityUser, roles[i].Name).Result;
                    if (selectedRoles[i] && !hasRole)
                    {
                        // User doesn't have the role yet, add it:
                        var r1 = _userManager.AddToRoleAsync(identityUser, roles[i].Name);
                    }
                    else if (!selectedRoles[i] && hasRole)
                    {
                        // User has the role, but it was unchecked in model.
                        // Remove the role from the user:
                        var r2 = _userManager.RemoveFromRoleAsync(identityUser, roles[i].Name);
                    }
                }
                // TEST INFERNAL
                
                // FIN TEST INFERNAL
                //libUser.Instruments = null;
                var temp2 = _libraryUnitOfWork.LibUserRepository.Update(libUser);
                var temp3 = _userManager.UpdateAsync(identityUser);

                return RedirectToAction(nameof(UsersIndex));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult InstrumentsIndex()
        {
            var instruments = _libraryUnitOfWork.InstrumentRepository.GetAll().OrderBy(x => x.Name);
            return View(instruments);
        }

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
    }
}
