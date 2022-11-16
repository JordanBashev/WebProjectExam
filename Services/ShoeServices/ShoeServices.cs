using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebProjectExam.Database;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels;

namespace WebProjectExam.Services.ShoeServices
{
    public class ShoeServices : IShoeServices
    {
        private readonly ShoeStoreDbContext _context;
        public ShoeServices(ShoeStoreDbContext context)
        {
            _context = context;
        }

        public void Delete(int Id)
        {
            var shoeToDelete = _context.Shoes.FirstOrDefault(x => x.Id == Id);
            if(shoeToDelete != null)
            {
                _context.Shoes.Remove(shoeToDelete);
                _context.SaveChanges();
            }
        }

        public void Edit(EditShoeVM shoemodel)
        {
            if(shoemodel.Id == 0)
            {
                var shoeToCreate = new Shoe()
                {
                    Size = shoemodel.Size,
                    Price = new Price {
                        price = shoemodel.Price,
                        Shoe_Id = shoemodel.Id
                    },
                    Brand = new Brand {
                        Name = shoemodel.Brand,
                        Shoe_Id = shoemodel.Id},

                    Colour = shoemodel.Colour

                };
                
                
                _context.Shoes.Add(shoeToCreate);
              
            }
            else
            {
                var ShoeToEdit = _context.Shoes.FirstOrDefault(s => s.Id == shoemodel.Id);

                var shoePriceToEdit = _context.Prices.FirstOrDefault(p => p.Shoe_Id == shoemodel.Id);
                var shoeBrandToEdit = _context.Brands.FirstOrDefault(p => p.Shoe_Id == shoemodel.Id);

                if (ShoeToEdit != null)
                {

                    ShoeToEdit.Size = shoemodel.Size ;
                    ShoeToEdit.Colour = shoemodel.Colour;
                    shoePriceToEdit.price = shoemodel.Price;
                    shoeBrandToEdit.Name = shoemodel.Brand;

                    _context.Shoes.Update(ShoeToEdit);
                    _context.Prices.Update(shoePriceToEdit);
                    _context.Brands.Update(shoeBrandToEdit);
                }
            }
            _context.SaveChanges();
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


        public List<ShoeVM> GetAllShoes()
        {
            var shoes = _context.Shoes.Select(MapToShoeVM()).ToList();

            return shoes;
        }

        public Price GetPriceByShoe(ShoeVM shoe)
        {


            var price = _context.Prices.FirstOrDefault(p  => p.Shoe_Id == shoe.Id);
            return price;
        }

        public Brand GetBrandByShoe(ShoeVM shoe)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Shoe_Id == shoe.Id);
            return brand;


        }

    }
}