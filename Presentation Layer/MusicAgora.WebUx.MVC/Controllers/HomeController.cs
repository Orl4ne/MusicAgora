using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Identity.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.WebUx.MVC.Models;

namespace MusicAgora.WebUx.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILibraryUnitOfWork _libraryUnitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<AccessRight> _roleManager;

        public HomeController(ILogger<HomeController> logger,
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

        public IActionResult HomeIndex()
        {
            var isSignedIn = _signInManager.IsSignedIn(User);
            ApplicationUser user;
            if (isSignedIn)
            {
                user = _userManager.GetUserAsync(User).Result;
                return View(user);
            }
            user = null;
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
