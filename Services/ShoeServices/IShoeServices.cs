using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels;
using WebProjectExam.Models.ViewModels.TagVMs;

namespace WebProjectExam.Services.ShoeServices
{
    public interface IShoeServices : IDisposable
    {
        public string Stream(string link);
        public void StreamToReadAndDeleteFile(int Id);
        public void Edit(EditShoeVM shoemodel);
        public void Delete(int Id);
        public void AddToOrder(int Id);
        public Tag GetTagByShoeVM(ShoeVM shoe);
        public Tag GetTagByShoe(Shoe shoe);
        public Price GetPriceByShoe(Shoe shoe);
        public Brand GetBrandByShoe(Shoe shoe);
        public Image GetImageByShoe(Shoe shoe);
        public Image GetImageByShoeVM(ShoeVM shoe);
        public Brand GetBrandByShoeVM(ShoeVM shoe);
        public Price GetPriceByShoeVM(ShoeVM shoe);
        public List<ShoeVM> GetAllShoes();
        public IEnumerable<TagVM> GetAllTags();
        public Shoe GetShoeById(int Id);
        public ShoeVM GetShoeByIdAndUserIdVM(int Id, string userId);
    }
}

