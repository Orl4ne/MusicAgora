﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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


        #region Sheets Actions
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
        public IActionResult CreateSheet(LibraryVM libraryVM)
        {
            var sheet = libraryVM.Sheet;
            var cat = _libraryUnitOfWork.CategoryRepository.GetAll().First(x => x.Name == libraryVM.SelectedCategory);
            sheet.Category = cat;
            _librarianUC.CreateANewSheet(sheet);
            return RedirectToAction("AllSheets");
        }

        [HttpGet]
        [Authorize(Roles = "Librarian")]
        public IActionResult SheetDetails(int id)
        {
            var sheet = _libraryUnitOfWork.SheetRepository.GetById(id);

            var sheetParts = _chiefUC?.GetAllSheetPartsBySheet(id);
            var libraryVM = new LibraryVM
            {
                Sheet = sheet,
                SheetPartsFromSheet = sheetParts,
            };
            return View(libraryVM);
        }
        #endregion

        #region SheetParts Actions
        [HttpGet]
        [Authorize(Roles = "Librarian")]
        public IActionResult UploadSheetPartToSheet()
        {
            var libraryVM = new LibraryVM
            {
                AllSheets = _libraryUnitOfWork.SheetRepository.GetAll().ToList(),
                AllInstruments = _libraryUnitOfWork.InstrumentRepository.GetAll().ToList(),
            };
            return View(libraryVM);
        }

        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public IActionResult UploadSheetPartToSheet(LibraryVM libraryVM)
        {
            var selectedInstruments = libraryVM.AllInstruments.Where(inst => inst.IsSelected == true).ToList();
            var sheet = _chiefUC.GetAllSheets().FirstOrDefault(x => x.Name == libraryVM.SelectedSheet);
            
            var file = libraryVM.SheetPartFile;
            var stream = file.OpenReadStream();

            foreach (var selectedInstru in selectedInstruments)
            {
                var instru = _libraryUnitOfWork.InstrumentRepository.GetAll().FirstOrDefault(x=>x.Id == selectedInstru.Id);
                var sheetPart = new SheetPartTO
                {
                    Instrument = instru,
                    Part = libraryVM.SheetPart.Part,
                    Sheet = sheet
                };
                var uploadedSheetPart = _librarianUC.UploadSheetPartInSheet(sheetPart, stream);
            }
            return RedirectToAction("LibraryIndex");
        }

        [HttpGet]
        [Authorize(Roles = "Musician")]
        public FileResult DownloadSheetPart(int id)
        {
            var sheetPart = _libraryUnitOfWork.SheetPartRepository.GetById(id);
            var completePath = _musicianUC.DowloadSheetPart(id);
            var reg = new Regex("\\W");
            var fileName = $@"{reg.Replace(sheetPart.Sheet.Name, "_")}-{reg.Replace(sheetPart.Part, "_")}.pdf";
            byte[] fileBytes = System.IO.File.ReadAllBytes(completePath);
            return File(fileBytes, "application/pdf", fileName);
        }

        [HttpGet]
        [Authorize(Roles = "Musician")]
        public IActionResult MySheetPartsOfThisSheet(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var result = _musicianUC.GetMySheetPartsForThisSheet(user.Id, id);
            return View(result);
        }
        #endregion

        #region Categories Actions
        [HttpGet]
        [Authorize(Roles = "Librarian")]
        public IActionResult AllCategories()
        {
            var allCategories = _libraryUnitOfWork.CategoryRepository.GetAll();
            return View(allCategories);
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
            return RedirectToAction("AllCategories");
        }

        [HttpGet]
        [Authorize(Roles = "Librarian")]
        public IActionResult EditCategory (int id)
        {
            var category = _libraryUnitOfWork.CategoryRepository.GetById(id);
            return View(category);
        }
        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public IActionResult EditCategory(int id, CategoryTO category)
        {
            _libraryUnitOfWork.CategoryRepository.Update(category);
            return RedirectToAction("AllCategories");
        }

        [HttpGet]
        [Authorize(Roles = "Librarian")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _libraryUnitOfWork.CategoryRepository.GetById(id);
            return View(category);
        }
        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public IActionResult DeleteCategory(int id, CategoryTO category)
        {
            _libraryUnitOfWork.CategoryRepository.Delete(category);
            return RedirectToAction("AllCategories");
        }
        #endregion
    }
}

