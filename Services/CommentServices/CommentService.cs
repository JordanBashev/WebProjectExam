using System;
using WebProjectExam.Database;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels.CommentVM;
using System.Linq;
using System.Collections.Generic;

namespace WebProjectExam.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly ShoeStoreDbContext _context;

        public CommentService(ShoeStoreDbContext context)
        {
            _context = context;
        }

        public void Create(CommentVM comment)
        {
            var commentToCreate = new Comment();
            commentToCreate.ShoeId = comment.Shoe_Id;
            commentToCreate.UserId = comment.User_Id;
            commentToCreate.Description = comment.Decriptions;
            _context.Comments.Add(commentToCreate);
            _context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var commentToDelete = _context.Comments.FirstOrDefault(x => x.Id == Id);
            _context.Comments.Remove(commentToDelete);
        }

        public void Edit(CommentVM comment)
        {
            var commentToEdit = _context.Comments.FirstOrDefault(x => x.Id == comment.Id);
            if(commentToEdit != null)
            {
                commentToEdit.Description = comment.Decriptions;
                _context.Comments.Update(commentToEdit);
                _context.SaveChanges();
            }
        }

        public List<Comment> ShowAllComments()
        {
            return _context.Comments.ToList();
        }

        public List<Comment> ShowAllShoeComments(int id)
        {
            var getShoeId = _context.Shoes.FirstOrDefault(x => x.Id == id);
            return _context.Comments.Where(x => x.ShoeId == getShoeId.Id).ToList();
        }

    }
}

