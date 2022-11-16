using System;
using System.Collections.Generic;
using System.Linq;
using WebProjectExam.Models.Entities;

namespace WebProjectExam.Models.ViewModels
{
    public class ShoeVM
    {
        public int Id { get; set; }
        public Brand Brand { get; set; }
        public Price Price { get; set; }
        public string Colour { get; set; }
        public double Size { get; set; }
        public string uri { get; set; }

        public List<ShoeVM> Shoes { get; set; }
    }
}

