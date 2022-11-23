using System;
using System.Collections.Generic;
using System.Linq;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels.CommentVM;

namespace WebProjectExam.Services.CommentServices
{
	public interface ICommentService
	{
		public void Create(CommentVM comment);
		public void Edit(CommentVM comment);
		public void Delete(int Id);
		public List<Comment> ShowAllComments();
        public List<Comment> ShowAllShoeComments(int id);
		public Comment GetCommentById(int id);

    }
}

