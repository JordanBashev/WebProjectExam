using System;
using System.Collections.Generic;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels;

namespace WebProjectExam.Services.ShoeServices
{
    public interface IShoeServices
    {
        
        public void Edit(EditShoeVM shoemodel);
        public void Delete(int Id);
        public Brand GetBrandByShoe(ShoeVM shoe);
        public Price GetPriceByShoe(ShoeVM shoe);
        public List<ShoeVM> GetAllShoes();
    }
}

