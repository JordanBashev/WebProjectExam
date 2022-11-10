using System;
using System.Collections.Generic;
using System.Linq;
using WebProjectExam.Models.Entities;

namespace WebProjectExam.Models.ViewModels
{
    public class ShoeVM
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Colour { get; set; }
        public double Size { get; set; }

        public List<ShoeVM> Shoes { get; set; }
    }
}

