using System;
namespace WebProjectExam.Services.OrderSevices
{
    public interface IOrderServices
    {
        public void seedOrders();
        public void Edit(int Id );
        public void Delete(int Id);

    }
}

