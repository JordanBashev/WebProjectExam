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

    }
}

