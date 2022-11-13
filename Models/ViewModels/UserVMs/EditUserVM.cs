using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebProjectExam.Models.ViewModels.UserVMs
{
    public class EditUserVM
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public IdentityRole Role { get; set; }

        public IEnumerable<EditRolesVM> Roles { get; set; }

    }
}
