using System.Collections.Generic;

namespace WebProjectExam.Models.ViewModels.UserVMs
{
    public class ShowAllUsersVM
    {
        public IEnumerable<UserVM> Users { get; set; }
    }
}
