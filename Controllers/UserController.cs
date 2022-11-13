using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using WebProjectExam.Models.ViewModels.UserVMs;
using WebProjectExam.Services.UserServices;

namespace WebProjectExam.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult Create(EditUserVM model)
        {
            model.Roles = _userServices.GetAllRoles();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(EditUserVM model, string id)
        {
            if (ModelState.IsValid)
            {
                _userServices.Edit(model);
            }
            return RedirectToAction(nameof(AllUsers));
        }

        [HttpGet]
        public IActionResult Edit(EditUserVM model, string Id)
        {
            model.Roles = _userServices.GetAllRoles();
            var currUser = _userServices.FindUser(Id);
            model.Username = currUser.UserName;
            model.PhoneNumber = currUser.PhoneNumber;
            model.EmailAddress = currUser.Email;
            model.Role = _userServices.FindUserRoleById(Id);
            if (model.Role == null)
            {
                model.Role.Name = "None";              
            }
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditUserVM model)
        {
            if (ModelState.IsValid)
            {
                var checkIsUserCustomer = _userServices.FindUserRoleById(model.Id);
                if(checkIsUserCustomer.Name == "Customer")
                {
                    model.Role = checkIsUserCustomer;
                }
                
                _userServices.Edit(model);
            }
            return RedirectToAction(nameof(AllUsers));
        }

        public IActionResult Delete(string id)
        {
            _userServices.Delete(id);
            return RedirectToAction(nameof(AllUsers));
        }

        [HttpGet]
        public IActionResult AllUsers(ShowAllUsersVM model)
        {
            if (ModelState.IsValid)
            {
                model.Users = _userServices.GetAll();
                foreach (var user in model.Users)
                {
                    var Role = _userServices.FindUserRole(user);
                    if (Role != null)
                    {
                        user.Role = Role.Name;
                    }
                    else
                    {
                        user.Role = "None";
                    }
                }
            }
            return View(model);
        }

    }
}
