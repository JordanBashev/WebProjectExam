using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjectExam.Models.Entities
{
    public class User : IdentityUser
    {
        //Foreign key
        //recives Ids of orders for this user
        public int OrdersId { get; set; }
    }
}
