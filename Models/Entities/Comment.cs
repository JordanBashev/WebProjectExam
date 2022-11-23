using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectExam.Models.Entities
{
	public class Comment
	{
		public int Id { get; set; }
		public string  UserId { get; set; }
		public int ShoeId { get; set; }
		public string  Description { get; set; }

		[ForeignKey(nameof(ShoeId))]
		public Shoe Shoe { get; set; }
	}
}

