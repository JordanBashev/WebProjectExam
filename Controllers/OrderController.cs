using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels;
using WebProjectExam.Services.OrderSevices;
using WebProjectExam.Services.ShoeServices;

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
            var orders = _orderServices.GetAll();
            var orderCount = orders.Count();
            allOrderedShoes.OrderedShoes = _orderServices.getOrderedShoes();

            foreach (var shoe in allOrderedShoes.OrderedShoes)
            {
                shoe.Brand = _shoeServices.GetBrandByShoe(shoe);
                shoe.Price = _shoeServices.GetPriceByShoe(shoe);
                var TagExists = _shoeServices.GetTagByShoe(shoe);
                if (TagExists != null)
                {
                    shoe.Tag = TagExists;
                }
                else
                {
                    shoe.Tag = null;
                }
                shoe.uri = _shoeServices.GetImageByShoe(shoe);
                var getOrderStatus = _orderServices.getOrderByShoe(shoe);
                shoe.order = getOrderStatus;
            }
            return View(allOrderedShoes);
        }

        public IActionResult AllOrders(ShoeVM allOrderedShoes)
        {
            allOrderedShoes.OrderedShoes = _orderServices.get_All_Ordered_Shoes();
            foreach (var shoe in allOrderedShoes.OrderedShoes)
            {
                shoe.Brand = _shoeServices.GetBrandByShoe(shoe);
                shoe.Price = _shoeServices.GetPriceByShoe(shoe);
                var TagExists = _shoeServices.GetTagByShoe(shoe);
                if (TagExists != null)
                {
                    shoe.Tag = TagExists;
                }
                else
                {
                    shoe.Tag = null;
                }
                shoe.uri = _shoeServices.GetImageByShoe(shoe);
                var getOrderStatus = _orderServices.getOrderByShoe(shoe);
                shoe.order = getOrderStatus;
            }
            return View(allOrderedShoes);
        }

        public IActionResult EditStatus(ShoeVM shoe ,int Id)
        {
            var getShoeToEditStatus = _shoeServices.GetShoeById(Id);
            var getOrderStatus = _orderServices.getOrderByShoe(getShoeToEditStatus);
            shoe.Status = getOrderStatus.Status;
            return View(shoe);
        }

        [HttpPost]
        public IActionResult EditStatus(string Status)
        {
            List<string> value = Status.Split("+").ToList();
            if (ModelState.IsValid)
            {
                _orderServices.Edit(int.Parse(value[1]), value[0]);
            }
            return RedirectToAction(nameof(AllOrders));
        }

        public IActionResult Delete(int id)
        {
            _orderServices.Delete(id);
            return RedirectToAction(nameof(AllUserOrders));
        }
    }
}
