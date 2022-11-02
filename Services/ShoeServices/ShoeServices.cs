using System;
using System.Linq;
using WebProjectExam.Database;
using WebProjectExam.Models.Entities;

namespace WebProjectExam.Services.ShoeServices
{
    public class ShoeServices : IShoeServices
    {
        private readonly ShoeStoreDbContext _context;
        public ShoeServices(ShoeStoreDbContext context)
        {
            _context = context;
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

