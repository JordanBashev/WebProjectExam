using System;
using System.Collections.Generic;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels;

namespace WebProjectExam.Services.ShoeServices
{
    public interface IShoeServices
    {
        public string Stream(string link);
        public void Edit(EditShoeVM shoemodel);
        public void Delete(int Id);
        public Image GetImageByShoe(ShoeVM shoe);
        public Brand GetBrandByShoe(ShoeVM shoe);
        public Price GetPriceByShoe(ShoeVM shoe);
        public List<ShoeVM> GetAllShoes();
    }
}

