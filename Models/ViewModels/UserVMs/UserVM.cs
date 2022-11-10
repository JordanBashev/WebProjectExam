using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WebProjectExam.Models.Entities;

namespace WebProjectExam.Models.ViewModels.UserVMs
{
    public class UserVM 
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string EmailAddress { get; set; }

        public string Role { get; set; }
    }
}
