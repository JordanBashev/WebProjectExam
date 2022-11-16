using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectExam.Models.Entities
{
	public class Image
	{
		public int Id { get; set; }
		public string uri { get; set; }
        public int Shoe_Id { get; set; }

        [ForeignKey(nameof(Shoe_Id))]
        public Shoe Shoe { get; set; }
    }
}

