using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.WebUx.MVC.Models;

namespace MusicAgora.WebUx.MVC.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILibraryUnitOfWork _libraryUnitOfWork;
        private readonly IChiefUC _chiefUC;
        private readonly ILibrarianUC _librarianUC;
        private readonly IMusicianUC _musicianUC;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AdminController(ILogger<HomeController> logger,
            ILibraryUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IChiefUC chiefUC,
            ILibrarianUC librarianUC,
            IMusicianUC musicianUC)
        {
            _logger = logger;
            _libraryUnitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _chiefUC = chiefUC;
            _librarianUC = librarianUC;
            _musicianUC = musicianUC;
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


    }
}
