using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjectExam.Services.UserServices
{
    public interface IUserServices
    {
        public void SeedRoles();
        public void SeedAdmin();

    }
}
