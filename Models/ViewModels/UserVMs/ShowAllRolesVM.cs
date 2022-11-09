using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebProjectExam.Models.ViewModels.UserVMs
{
    public class ShowAllRolesVM
    {
        public IEnumerable<EditRolesVM> Roles { get; set; }
    }
}
