using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels;
using WebProjectExam.Services.OrderSevices;
using WebProjectExam.Services.ShoeServices;
using WebProjectExam.Services.UserServices;

namespace WebProjectExam.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly IShoeServices _shoeServices;

        public OrderController(IOrderServices orderServices, IShoeServices shoeServices)
        {
            _orderServices = orderServices;
            _shoeServices = shoeServices;
        }

        public IActionResult AllUserOrders(ShoeVM allOrderedShoes)
        {
            allOrderedShoes.UserOrderedShoes = _orderServices.getUserOrderedShoes();
            return View(allOrderedShoes);
        }

        public IActionResult AllOrders(ShoeVM allOrderedShoes)
        {
            allOrderedShoes.OrderedShoes = _orderServices.get_All_Ordered_ShoesVM();               
            return View(allOrderedShoes);
        }

        [HttpGet]
        public IActionResult EditStatus(ShoeVM shoe ,string Id)
        {
            var getCurrOrder = _orderServices.getOrderByUserId(Id);
            shoe.Status = getCurrOrder.Status;
            shoe.userId = getCurrOrder.user_Id;
            shoe.Id = getCurrOrder.shoe_Id;
            return View(shoe);
        }

        [HttpPost]
        public IActionResult EditStatus(string Status)
        {
            List<string> value = Status.Split("+").ToList();
            if (ModelState.IsValid)
            {
                _orderServices.Edit(value[1], value[0]);
            }
            return RedirectToAction(nameof(AllOrders));
        }

        public IActionResult Delete(string id)
        {
            _orderServices.Delete(id);
            return RedirectToAction(nameof(AllUserOrders));
        }
    }
}
