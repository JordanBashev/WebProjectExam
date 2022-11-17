using System;
using System.Collections.Generic;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels;
using WebProjectExam.Models.ViewModels.OrderVMs;

namespace WebProjectExam.Services.OrderSevices
{
    public interface IOrderServices
    {
        public void Delete(int Id);
        public void Edit(int Id, string status);
        public IEnumerable<OrderVM> GetAll();
        public IEnumerable<Shoe> get_All_Ordered_Shoes();
        public IEnumerable<OrderVM> getAllOrderForLoggedUser();
        public IEnumerable<ShoeVM> getOrderedShoesVM();
        public IEnumerable<Shoe> getOrderedShoes();
        public Order getOrderByShoe(Shoe shoe);
        public Order getOrderById(int Id);
    }
}

