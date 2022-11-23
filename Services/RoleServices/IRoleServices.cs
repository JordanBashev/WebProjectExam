using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using WebProjectExam.Models.ViewModels.RoleVMs;

namespace WebProjectExam.Services.RoleServices
{
    public interface IRoleServices
    {
        public IdentityRole GetRoleById(string Id);
        public IEnumerable<IdentityRole> GetAllRoles();
        public void Edit(RoleVm role);
        public void Delete(string Id);
            
    }
}   
