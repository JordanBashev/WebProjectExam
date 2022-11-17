using System;
namespace WebProjectExam.Models.Entities
{
    public class Shoe
    {
        public int Id { get; set; }
        public Price Price { get; set;}
        public Tag Tag { get; set; }
        public Brand Brand { get; set; }
        public string Colour { get; set; }
        public double  Size { get; set; }
        public Image uri { get; set; }
        public Order order { get; set; }

    }
}

