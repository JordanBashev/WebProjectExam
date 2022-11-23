using System;
using System.Collections.Generic;
using WebProjectExam.Models.Entities;

namespace WebProjectExam.Models.ViewModels.CommentVM
{
	public class AllCommentsVM
	{
		public List<Comment> Comments { get; set; }

		public int Id { get; set; }
	}
}

