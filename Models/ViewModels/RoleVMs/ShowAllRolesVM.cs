using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace WebProjectExam.Models.ViewModels.RoleVMs
{
    public class ShowAllRolesVm
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
