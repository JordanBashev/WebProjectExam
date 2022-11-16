using System;
using WebProjectExam.Models.Entities;

namespace WebProjectExam.Models.ViewModels
{
	public class EditShoeVM
	{
        public int Id { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Colour { get; set; }
        public double Size { get; set; }
    }
}

