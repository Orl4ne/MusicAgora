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
using MusicAgora.WebUx.MVC.Models;

namespace MusicAgora.WebUx.MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILibraryUnitOfWork _libraryUnitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<AccessRight> _roleManager;
        //private readonly IdentityRole _identityRole;


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
            //_identityRole = identityRole;
        }
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
                    LibraryUser = libraryUsers.First(x => x.IdentityUserId == identityUser.Id)
                };
                globalUsers.Add(globalUserVM);
            }
            return View(globalUsers);
        }

        [HttpGet]
        public IActionResult InstrumentsIndex()
        {
            var instruments = _libraryUnitOfWork.InstrumentRepository.GetAll();
            return View(instruments);
        }


    }
}
