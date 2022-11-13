using Microsoft.AspNetCore.Mvc;
using WebProjectExam.Models.ViewModels.RoleVMs;
using WebProjectExam.Services.RoleServices;

namespace WebProjectExam.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleServices _roleService;

        public RoleController(IRoleServices roleService)
        {
            _roleService = roleService;
        }

        public IActionResult AllRoles(ShowAllRolesVm allRoles)
        {
            allRoles.Roles = _roleService.GetAllRoles();

            return View(allRoles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoleVm role)
        {
            if (ModelState.IsValid)
            {
                _roleService.Edit(role);
            }
            return RedirectToAction(nameof(AllRoles));
        }

        [HttpGet]
        public IActionResult Edit(RoleVm role, string Id)
        {
            var roleToEdit = _roleService.GetRoleById(role.Id);
            role.Name = roleToEdit.Name;
            return View(role);
        }

        [HttpPost]
        public IActionResult Edit(RoleVm role)
        {
            if(ModelState.IsValid)
            {
                _roleService.Edit(role);
            }
            return RedirectToAction(nameof(AllRoles));
        }

        public IActionResult Delete(string Id)
        {
            _roleService.Delete(Id);
            return RedirectToAction(nameof(AllRoles));
        }
    }
}
