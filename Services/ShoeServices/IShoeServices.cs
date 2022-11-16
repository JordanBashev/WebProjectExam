using System;
using System.Collections.Generic;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels;

namespace WebProjectExam.Services.ShoeServices
{
    public interface IShoeServices
    {
        public void Create(ShoeVM shoemodel);
        public void Edit(ShoeVM shoemodel);
        public void Delete(int Id);
        public List<ShoeVM> GetAllShoes();
    }
}

