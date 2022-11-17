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

        public void Delete(int Id)
        {
            var Order = _context.orders.FirstOrDefault(x => x.shoe_Id == Id);
            _context.orders.Remove(Order);
            _context.SaveChanges();
        }

        public IEnumerable<OrderVM> GetAll()
        {
            var Orders = _context.orders.Select(MapToOrderVM()).ToList();

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
                    orderedShoes.Add(_shoeService.GetShoeById(shoe.Shoe_Id));
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

        public IEnumerable<Shoe> getOrderedShoes()
        {
            var userOrders = getAllOrderForLoggedUser();
            List<Shoe> orderedShoes = new List<Shoe>();
            using (var _shoeService = new ShoeServices.ShoeServices(_context, _signInManager))
            {
                foreach (var shoe in userOrders)
                {
                    orderedShoes.Add(_shoeService.GetShoeById(shoe.Shoe_Id));
                }
            }
            return orderedShoes;
        }

        public IEnumerable<ShoeVM> getOrderedShoesVM()
        {
            var orders = getOrderedShoes();
            List<ShoeVM> orderedShoesVM = new List<ShoeVM>();
            foreach (var shoeVM in orderedShoesVM)
            {
                foreach (var shoe in orders)
                {
                    shoeVM.Brand = shoe.Brand;
                    shoeVM.Price = shoe.Price;
                    shoeVM.Colour = shoe.Colour;
                    shoeVM.Size = shoe.Size;
                    shoeVM.Status = shoe.order.Status;
                    shoeVM.uri = shoe.uri.uri;
                }
                orderedShoesVM.Add(shoeVM);
            }
            return orderedShoesVM;
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

        public void Edit(int Id, string status)
        {
            var shoeStatusToEdit = _context.orders.FirstOrDefault(x => x.shoe_Id == Id);
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

