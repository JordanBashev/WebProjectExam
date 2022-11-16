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
        public IActionResult ShowShoes(ShoeVM shoemodel)
        {
            
            shoemodel.Shoes = shoeService.GetAllShoes();
            foreach(var shoe in shoemodel.Shoes)
            {
                shoe.Price = shoeService.GetPriceByShoe(shoe);
                shoe.Brand = shoeService.GetBrandByShoe(shoe);
            }
            return View("Index" , shoemodel);

        }
        public IActionResult ShowCustomerShoes(ShoeVM shoemodel)
        {

            shoemodel.Shoes = shoeService.GetAllShoes();
            foreach (var shoe in shoemodel.Shoes)
            {
                shoe.Price = shoeService.GetPriceByShoe(shoe);
                shoe.Brand = shoeService.GetBrandByShoe(shoe);
            }
            return View("CustomerIndex", shoemodel);

        }

        [HttpPost]
        public IActionResult Edit(EditShoeVM shoe)
        {
            shoeService.Edit(shoe);
            return RedirectToAction(nameof(ShowShoes)); 
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EditShoeVM shoemodel)
        {
            if (ModelState.IsValid)
            {
             shoeService.Edit(shoemodel);
            }
            return RedirectToAction(nameof(ShowShoes));

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            shoeService.Delete(Id);
            return RedirectToAction(nameof(ShowShoes));
        }
    }
}

