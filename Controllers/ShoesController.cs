using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProjectExam.Models.ViewModels;
using WebProjectExam.Services.ShoeServices;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebProjectExam.Controllers
{
    public class ShoesController : Controller
    {

        private readonly IShoeServices shoeService;

        public ShoesController(IShoeServices shoeServices)
        {
            this.shoeService = shoeServices;
        }
        // GET: /<controller>/
        public IActionResult ShowShoes()
        {
            var shoemodel = new ShoeVM();

            shoemodel.Shoes = shoeService.GetAllShoes();
            return View("Index" , shoemodel);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ShoeVM shoemodel)
        {
            return View("Index" , "Create");
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {

            return View();
        }
    }
}

