using System.Collections.Generic;
using WebProjectExam.Models.Entities;

namespace WebProjectExam.Models.ViewModels.UserVMs
{
    public class ShowAllUsersVM
    {
        public IEnumerable<UserVM> Users { get; set; }
    }
}
