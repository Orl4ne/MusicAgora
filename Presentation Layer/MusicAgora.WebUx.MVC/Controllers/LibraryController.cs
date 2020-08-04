using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.DAL;
using Library.BLL.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;

namespace MusicAgora.WebUx.MVC.Controllers
{
    public class LibraryController : Controller
    {
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
        [HttpGet]
        [Authorize]
        public IActionResult LibraryIndex()
        {
            return View();
        }
    }
}
