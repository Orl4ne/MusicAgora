using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.DAL;
using Library.BLL.UseCases;
using Library.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.Common.Library.TransferObjects;
using MusicAgora.WebUx.MVC.Models;

namespace MusicAgora.WebUx.MVC.Controllers
{
    public class LibraryController : Controller
    {
        #region CTOR and Dependancies
        private readonly ILogger<HomeController> _logger;
        private readonly ILibraryUnitOfWork _libraryUnitOfWork;
        private readonly IChiefUC _chiefUC;
        private readonly ILibrarianUC _librarianUC;
        private readonly IMusicianUC _musicianUC;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public LibraryController(ILogger<HomeController> logger,
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
        #endregion

        [HttpGet]
        [Authorize]
        public IActionResult LibraryIndex()
        {
            var isSignedIn = _signInManager.IsSignedIn(User);
            ApplicationUser user;
            if (isSignedIn)
            {
                user = _userManager.GetUserAsync(User).Result;
                var libUser = _libraryUnitOfWork.LibUserRepository.GetByIdentityUserId(user.Id);
                var globalUser = new GlobalUserVM
                {
                    IdentityUser = user,
                    LibraryUser = libUser,
                };
                return View(globalUser);
            }
            return RedirectToAction("HomeIndex", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllCurrentSheets(int id)
        {
            var allCurrentSheets = _musicianUC.SeeAllCurrentSheets(id);
            return View(allCurrentSheets);
        }

        [HttpGet]
        [Authorize(Roles = "Librarian, Chief")]
        public IActionResult AllSheets(int id)
        {
            var allSheets = _chiefUC.GetAllSheets();
            return View(allSheets);
        }

        [HttpGet]
        [Authorize(Roles = "Librarian")]
        public IActionResult CreateSheet()
        {
            var allCategories = _libraryUnitOfWork.CategoryRepository.GetAll().ToList();
            var libraryVM = new LibraryVM
            {
                AllCategories = allCategories,
            };
            return View(libraryVM);
        }

        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public IActionResult CreateSheet(int id, LibraryVM libraryVM)
        {
            var sheet = libraryVM.Sheet;
            var cat = _libraryUnitOfWork.CategoryRepository.GetAll().First(x => x.Name == libraryVM.SelectedCategory);
            sheet.Category = cat;
            _librarianUC.CreateANewSheet(sheet);
            return RedirectToAction("AllSheets");
        }

        [HttpGet]
        [Authorize(Roles = "Librarian")]
        public IActionResult CreateNewCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public IActionResult CreateNewCategory(CategoryTO category)
        {
            var cat = _librarianUC.AddNewCategory(category);
            // TODO popup that says that the catery is added.
            return RedirectToAction(nameof(AllCategories));
        }

        [HttpGet]
        [Authorize(Roles = "Librarian")]
        public IActionResult AllCategories()
        {
            var allCategories = _libraryUnitOfWork.CategoryRepository.GetAll();
            return View(allCategories);
        }
    }
}

