using System;
using System.Linq;
using WebProjectExam.Database;
using WebProjectExam.Models.Entities;

namespace WebProjectExam.Services.OrderSevices
{
    public class OrderServices : IOrderServices
    {
        private readonly ShoeStoreDbContext _context;
        public OrderServices(ShoeStoreDbContext context)
        {
            _context = context;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public void Edit(int Id)
        {
            throw new NotImplementedException();
        }

        public void seedOrders()
        {
            var finduserbyname = _context.Users.FirstOrDefault(x => x.UserName == "admin");

            Order newOrder = new Order
            {
                user_Id = finduserbyname.Id
            };
            if(!_context.orders.Any(x => x.user_Id == newOrder.user_Id))
            {
                _context.orders.Add(newOrder);
                _context.SaveChanges();
            }

        }
    }
}

