using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels.CommentVM;
using WebProjectExam.Models.ViewModels.UserVMs;
using WebProjectExam.Services.CommentServices;
using WebProjectExam.Services.RoleServices;

namespace WebProjectExam.Controllers
{
	public class CommentController : Controller
	{
        private readonly ICommentService _commentService;
        private readonly SignInManager<User> _signInManager;

        public CommentController(ICommentService commentService, SignInManager<User> signInManager)
        {
            _commentService = commentService;
            _signInManager = signInManager;
        }

        
        public IActionResult AllComments(AllCommentsVM commentsVM)
        {
            commentsVM.Comments = _commentService.ShowAllComments();
            return View(commentsVM);
        }

        public IActionResult AllShoeComments(AllCommentsVM commentsVM)
        {
            commentsVM.Comments = _commentService.ShowAllShoeComments(commentsVM.Id);
            return View(commentsVM);
        }


        [HttpPost]
        public IActionResult Create(CommentVM commentVM, int id)
        {
            commentVM.Shoe_Id = id;
            commentVM.User_Id = _signInManager.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                _commentService.Create(commentVM);
            }
            var coment = new AllCommentsVM();
            coment.Id = id;
            return RedirectToAction(nameof(AllShoeComments), coment);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit (int Id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(CommentVM commentVM)
        {
            if (ModelState.IsValid)
            {
                _commentService.Edit(commentVM);
            }
            return RedirectToAction(nameof(AllComments));
        }

        public IActionResult Delete(int Id)
        {
            _commentService.Delete(Id);
            return RedirectToAction(nameof(Index));

        }


    }
}

