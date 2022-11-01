using System;
namespace WebProjectExam.Models.Entities
{
    public class Shoes
    {
        //Foreign key 
        public int Tag_Id { get; set; }
        
        public decimal Price { get; set; }
        public string Colour { get; set; }
        public double  Size { get; set; }

        //optional 
        public string Description { get; set; }

    }
}

