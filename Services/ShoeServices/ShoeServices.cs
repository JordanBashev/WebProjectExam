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

        public void Edit(ShoeVM shoemodel)
        {
            if(shoemodel.Id != 0)
            {
                var shoeToCreate = new Shoe()
                {
                    Size = shoemodel.Size,
                    Price = shoemodel.Price,
                    Colour = shoemodel.Colour

                };
                _context.Shoes.Add(shoeToCreate);
                
            }
            else
            {
                var ShoeToEdit = _context.Shoes.FirstOrDefault(s => s.Id == shoemodel.Id);

                var shoe = new Shoe()
                {
                    Size = shoemodel.Size,
                    Price = shoemodel.Price,
                    Colour = shoemodel.Colour
                };

                if(ShoeToEdit != null)
                {
                    ShoeToEdit = shoe;
                    _context.Shoes.Update(ShoeToEdit);
                }
            }
            _context.SaveChanges();
        }

        private static Expression<Func<Shoe, ShoeVM>> MapToShoeVM()
        {
            return x => new ShoeVM()
            {
                Id = x.Id,
                Price = x.Price,
                Size = x.Size,
                Colour = x.Colour
            };
        }


        public List<ShoeVM> GetAllShoes()
        {
            var shoes = _context.Shoes.Select(MapToShoeVM()).ToList();

            return shoes;
        }

        public void seedShoes()
        {
            Shoe shoe = new Shoe
            {
                Price = 69.420M ,
                Colour = "Magenta",
                Size = 43.5D
            };

            _context.Shoes.Add(shoe);
            _context.SaveChanges();
        }

    }
}