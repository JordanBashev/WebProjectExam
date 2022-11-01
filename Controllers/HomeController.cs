using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebProjectExam.Models;
using WebProjectExam.Services.UserServices;

namespace WebProjectExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserServices userServices)
        {
            _userServices = userServices;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _userServices.SeedRoles();
            _userServices.SeedAdmin();
            return View();
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
