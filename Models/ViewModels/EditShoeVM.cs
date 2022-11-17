using System;
using System.Collections;
using System.Collections.Generic;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels.TagVMs;

namespace WebProjectExam.Models.ViewModels
{
	public class EditShoeVM
	{
        public int Id { get; set; }
        public string Brand { get; set; }
        public Tag Tag { get; set; }
        public decimal Price { get; set; }
        public string Colour { get; set; }
        public double Size { get; set; }
        public string uri { get; set; }

        public IEnumerable<TagVM> Tags { get; set; }
    }
}

