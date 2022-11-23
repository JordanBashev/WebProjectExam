using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using WebProjectExam.Database;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels;
using WebProjectExam.Models.ViewModels.OrderVMs;
using WebProjectExam.Models.ViewModels.UserVMs;

namespace WebProjectExam.Services.OrderSevices
{
    public class OrderServices : IOrderServices
    {
        private readonly ShoeStoreDbContext _context;
        private readonly SignInManager<User> _signInManager;
        public OrderServices(ShoeStoreDbContext context, SignInManager<User> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public void Delete(string Id)
        {
            var Order = _context.orders.FirstOrDefault(x => x.user_Id == Id);
            _context.orders.Remove(Order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            var Orders = _context.orders.ToList();

            return Orders;
        }

        public IEnumerable<Shoe> get_All_Ordered_Shoes()
        {
            var Orders = GetAll();
            List<Shoe> orderedShoes = new List<Shoe>();
            using (var _shoeService = new ShoeServices.ShoeServices(_context, _signInManager))
            {
                foreach (var shoe in Orders)
                {
                    orderedShoes.Add(_shoeService.GetShoeById(shoe.shoe_Id));
                }
            }
            return orderedShoes;
        }


        public IEnumerable<OrderVM> getAllOrderForLoggedUser()
        {
            var userId = getUserId();
            var userOrders = _context.orders.Select(MapToOrderVM()).Where(x => x.User_Id == userId).ToList();

            return userOrders;
        }

        public IEnumerable<ShoeVM> getUserOrderedShoes()
        {
            var userOrders = getAllOrderForLoggedUser();
            List<ShoeVM> orderedShoes = new List<ShoeVM>();
            using (var _shoeService = new ShoeServices.ShoeServices(_context, _signInManager))
            {
                foreach (var shoe in userOrders)
                {
                    orderedShoes.Add(_shoeService.GetShoeByIdAndUserIdVM(shoe.Shoe_Id, shoe.User_Id));
                }
            }
            return orderedShoes;
        }

        public IEnumerable<ShoeVM> get_All_Ordered_ShoesVM()
        {
            var Orders = GetAll();
            List<ShoeVM> orderedShoes = new List<ShoeVM>();
            using (var _shoeService = new ShoeServices.ShoeServices(_context, _signInManager))
            {
                foreach (var order in Orders)
                {
                    orderedShoes.Add(_shoeService.GetShoeByIdAndUserIdVM(order.shoe_Id, order.user_Id));
                }
            }
            return orderedShoes;
        }

        public Order getOrderByShoe(Shoe shoe)
        {
            var order = _context.orders.FirstOrDefault(x => x.shoe_Id == shoe.Id);
            return order;
        }

        public Order getOrderById(int Id)
        {
            var order = _context.orders.FirstOrDefault(x => x.Id == Id);
            return order;
        }
        public Order getOrderByShoeVM(ShoeVM shoe)
        {
            var order = _context.orders.FirstOrDefault(x => x.shoe_Id == shoe.Id);
            return order;
        }

        public Order getOrderByUserId(string userId)
        {
            var order = _context.orders.FirstOrDefault(x => x.user_Id == userId);
            return order;
        }

        public void Edit(string Id, string status)
        {
            var shoeStatusToEdit = _context.orders.FirstOrDefault(x => x.user_Id == Id);
            if (shoeStatusToEdit != null)
            {
                shoeStatusToEdit.Status = status;
                _context.orders.Update(shoeStatusToEdit);
                _context.SaveChanges();
            }           
        }

        private static Expression<Func<Shoe, ShoeVM>> MapToShoeVM()
        {
            return x => new ShoeVM()
            {
                Id = x.Id,
                Size = x.Size,
                Colour = x.Colour
            };
        }

        private static Expression<Func<Order, OrderVM>> MapToOrderVM()
        {
            return x => new OrderVM()
            {
                Id = x.Id,
                User_Id = x.user_Id,
                Shoe_Id = x.shoe_Id
            };
        }

        private string getUserId()
        {
            return _signInManager.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}

