using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebProjectExam.Models.ViewModels;
using WebProjectExam.Services.OrderSevices;
using WebProjectExam.Services.ShoeServices;

namespace WebProjectExam.Controllers
{
    public class ShoesController : Controller
    {

        private readonly IShoeServices shoeService;
        private readonly IOrderServices orderServices;

        public ShoesController(IShoeServices shoeServices, IOrderServices orderServices)
        {
            this.shoeService = shoeServices;
            this.orderServices = orderServices;
        }

        public IActionResult ShowShoes(ShoeVM shoemodel)
        {

            shoemodel.Shoes = shoeService.GetAllShoes();
            foreach (var shoe in shoemodel.Shoes)
            {

                shoe.Price = shoeService.GetPriceByShoeVM(shoe);
                shoe.Brand = shoeService.GetBrandByShoeVM(shoe);
                var TagExists = shoeService.GetTagByShoeVM(shoe).Name;
                if (TagExists != null)
                {
                    shoe.Tag = TagExists;
                }
                else
                {
                    shoe.Tag = "None";
                }
                shoe.uri = shoeService.GetImageByShoeVM(shoe).uri;
            }
            return View("Index", shoemodel);

        }

        public IActionResult ShowCustomerShoes(ShoeVM shoemodel, string error)
        {

            shoemodel.Shoes = shoeService.GetAllShoes();
            foreach (var shoe in shoemodel.Shoes)
            {
                shoe.Price = shoeService.GetPriceByShoeVM(shoe);
                shoe.Brand = shoeService.GetBrandByShoeVM(shoe);
                var TagExists = shoeService.GetTagByShoeVM(shoe).Name;
                if (TagExists != null)
                {
                    shoe.Tag = TagExists;
                }
                else
                {
                    shoe.Tag = "None";
                }
                shoe.uri = shoeService.GetImageByShoeVM(shoe).uri;
            }
            if (error != null)
            {
                HttpContext.Response.WriteAsync(error);
            }
            return View("CustomerIndex", shoemodel);

        }

        [HttpPost]
        public IActionResult Edit(EditShoeVM shoe)
        {
            if (ModelState.IsValid)
            {
                shoeService.Edit(shoe);
            }
            return RedirectToAction(nameof(ShowShoes));
        }

        [HttpGet]
        public IActionResult Edit(EditShoeVM shoe, int Id)
        {
            var currShoe = shoeService.GetShoeById(Id);
            var currTag = shoeService.GetTagByShoe(currShoe);
            var currBrand = shoeService.GetBrandByShoe(currShoe);
            var currPrice = shoeService.GetPriceByShoe(currShoe);
            shoe.Size = currShoe.Size;
            shoe.Colour = currShoe.Colour;
            shoe.Price = currPrice.price;
            shoe.Brand = currBrand.Name;
            shoe.Tag = currTag;
            shoe.Tags = shoeService.GetAllTags();
            return View(shoe);
        }

        [HttpPost]
        public IActionResult Create(EditShoeVM shoemodel, string Id)
        {
            if (ModelState.IsValid)
            {
                shoeService.Edit(shoemodel);
            }
            return RedirectToAction(nameof(ShowShoes));

        }

        [HttpGet]
        public IActionResult Create(EditShoeVM shoemodel)
        {
            shoemodel.Tags = shoeService.GetAllTags();
            return View(shoemodel);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            shoeService.Delete(Id);
            return RedirectToAction(nameof(ShowShoes));
        }

        public IActionResult AddToOrder(int id)
        {
            var getShoe = shoeService.GetShoeById(id);
            var orderExists = orderServices.getOrderByShoe(getShoe);
            var error = @"<script language='javascript'>alert('Product is already added to cart'); </script>";
            if (orderExists == null)
            {
                shoeService.AddToOrder(id);
                return RedirectToAction(nameof(ShowCustomerShoes));
            }
            else
            {
                return RedirectToAction(nameof(ShowCustomerShoes), "Shoes", new {error});
            }
        }
    }
}

