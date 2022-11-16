using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectExam.Models.Entities
{
	public class Price
	{
        
        public int Id { get; set;}
        public int Shoe_Id { get; set; }
        public decimal price { get; set; }

        [ForeignKey(nameof(Shoe_Id))]
        public Shoe Shoe { get; set; }

        
    }
}

