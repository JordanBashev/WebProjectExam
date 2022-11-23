using System;
using System.Collections.Generic;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels;
using WebProjectExam.Models.ViewModels.OrderVMs;

namespace WebProjectExam.Services.OrderSevices
{
    public interface IOrderServices
    {
        public void Delete(string Id);
        public void Edit(string Id, string status);
        public IEnumerable<Order> GetAll();
        public IEnumerable<Shoe> get_All_Ordered_Shoes();
        public IEnumerable<ShoeVM> get_All_Ordered_ShoesVM();
        public IEnumerable<OrderVM> getAllOrderForLoggedUser();
        public IEnumerable<ShoeVM> getUserOrderedShoes();
        public Order getOrderByShoe(Shoe shoe);
        public Order getOrderByShoeVM(ShoeVM shoe);
        public Order getOrderById(int Id);
        public Order getOrderByUserId(string userId);
    }
}

