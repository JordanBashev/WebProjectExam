using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels.UserVMs;

namespace WebProjectExam.Services.UserServices
{
    public interface IUserServices
    {
        public void SeedRoles();
        public void SeedAdmin();
        public void Edit(EditUserVM userToEdit);
        public void Delete(string Id);
        public IEnumerable<UserVM> GetAll();
        public IEnumerable<EditRolesVM> GetAllRoles();
        public User FindUser(string Id);
        public IdentityRole FindUserRole(UserVM user);
        public IdentityRole FindUserRoleById(string Id);

    }
}
