using System;
namespace WebProjectExam.Models.Entities
{
    public class Shoe
    {
        public int Id { get; set; }
        public Price Price { get; set;}
        public Brand Brand { get; set; }
        public string Colour { get; set; }
        public double  Size { get; set; }

        
    }
}

