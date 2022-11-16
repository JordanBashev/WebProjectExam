using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebProjectExam.Models;
using WebProjectExam.Services.OrderSevices;
using WebProjectExam.Services.ShoeServices;
using WebProjectExam.Services.UserServices;

namespace WebProjectExam.Controllers
{
    public class HomeController : Controller
    {
        private int counter = 0;
        private readonly IUserServices _userServices;
        private readonly IOrderServices _orderServices;
        private readonly IShoeServices _shoeServices;
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(ILogger<HomeController> logger, IUserServices userServices , IOrderServices orderServices , IShoeServices shoeServices)
        {
            _userServices = userServices;
            _logger = logger;
            _orderServices = orderServices;
            _shoeServices = shoeServices;
            
        }

        public IActionResult Index()
        {
            if(counter == 0)
            {
                _userServices.SeedRoles();
                _userServices.SeedAdmin();
                counter++;
            }
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
