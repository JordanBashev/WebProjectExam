using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjectExam.Database;
using WebProjectExam.Models.Entities;

namespace WebProjectExam.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly ShoeStoreDbContext _context;
        public UserServices(ShoeStoreDbContext context)
        {
            _context = context;
        }

        //Adds roles on initial run
        public void SeedRoles()
        {
            string[] roles = { "Admin", "Worker" };

            if (!_context.Roles.Any(r => r.Name == "admin"))
            {
                foreach (string role in roles)
                {
                    IdentityRole identityRole = new IdentityRole()
                    {
                        Name = role,
                        NormalizedName = role.ToLower()
                    };

                    if (!_context.Roles.Any(r => r.Name == role))
                    {
                        _context.Roles.Add(identityRole);
                    }
                }
            }
            _context.SaveChanges();
        }

        //Adds admin on initial run
        //Always adds the first user added after first launch before adding anything to database
        public void SeedAdmin()
        {
            var getAdminId = _context.Roles.FirstOrDefault(x => x.Name == "Admin");

            var Roles = new IdentityUserRole<string>();
            var user = new User()
            {
                UserName = "Admin",
                NormalizedUserName = "Admin",
                Email = "Email@email.com",
                NormalizedEmail = "email@email.com",
                PhoneNumber = "+1111111111",
                EmailConfirmed = false,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //checks if user with given name exists if true dont add else add and make him admin
            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                Roles.RoleId = getAdminId.Id;
                Roles.UserId = user.Id;
                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(user, "adminpass");
                user.PasswordHash = hashed;
                _context.Users.Add(user);
                _context.UserRoles.Add(Roles);
            }
            _context.SaveChanges();
        }
    }
}
