using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebProjectExam.Database;
using WebProjectExam.Models.Entities;
using WebProjectExam.Models.ViewModels.UserVMs;

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
            string[] roles = { "Admin", "Worker", "Customer" };

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
        //Always adds the first user after first launch before adding anything to database
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

        //Adds new user if such user doesn't exists else edits the user and their role if changed
        public void Edit(EditUserVM userToEdit)
        {
            //finds user if he exists
            var checkIfExists = FindUser(userToEdit.Id);
            //gets the role to be changed/given if new user or editing
            var getRoleId = _context.Roles.FirstOrDefault(x => x.Id == userToEdit.Role.Id);

            if (checkIfExists == null)
            {
                //prepare new role and user to add
                var Roles = new IdentityUserRole<string>();
                var user = new User()
                {
                    UserName = userToEdit.Username,
                    NormalizedUserName = userToEdit.Username.ToLower(),
                    Email = userToEdit.EmailAddress,
                    NormalizedEmail = userToEdit.EmailAddress.ToLower(),
                    PhoneNumber = userToEdit.PhoneNumber,
                    EmailConfirmed = false,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //checks if user with given name exists if true dont add else add and give him the chosen role
                if (!_context.Users.Any(u => u.UserName == user.UserName))
                {
                    Roles.RoleId = getRoleId.Id;
                    Roles.UserId = user.Id;
                    var password = new PasswordHasher<User>();
                    var hashed = password.HashPassword(user, userToEdit.Password);
                    user.PasswordHash = hashed;
                    _context.Users.Add(user);
                    _context.UserRoles.Add(Roles);
                }
            }
            else
            {
                //Get the role of the user to change it
                var Roles = _context.UserRoles.FirstOrDefault(x => x.UserId == checkIfExists.Id);
                if (Roles != null)
                {
                    var isUserCustomer = _context.Roles.FirstOrDefault(x => x.Id == Roles.RoleId);
                    //Set changes 
                    checkIfExists.UserName = userToEdit.Username;
                    checkIfExists.NormalizedUserName = userToEdit.Username.ToLower();
                    checkIfExists.Email = userToEdit.EmailAddress;
                    checkIfExists.NormalizedEmail = userToEdit.EmailAddress.ToLower();
                    checkIfExists.PhoneNumber = userToEdit.PhoneNumber;
                    checkIfExists.EmailConfirmed = false;
                    checkIfExists.LockoutEnabled = false;


                    //Update the user data
                    _context.Users.Update(checkIfExists);
                    //Remove UserRole of the user and prepare to change their role if he is not Customer
                    if (isUserCustomer.Name != "Customer")
                    {
                        _context.UserRoles.Remove(Roles);
                        _context.SaveChanges();
                        //Set the new role thats given to the user and add
                        Roles.RoleId = getRoleId.Id;
                        Roles.UserId = checkIfExists.Id;
                        _context.UserRoles.Add(Roles);
                    }
                }
                else
                {
                    var userRoles = new IdentityUserRole<string>()
                    {
                        RoleId = getRoleId.Id,
                        UserId = checkIfExists.Id
                    };

                    //Set changes 
                    checkIfExists.UserName = userToEdit.Username;
                    checkIfExists.NormalizedUserName = userToEdit.Username.ToLower();
                    checkIfExists.Email = userToEdit.EmailAddress;
                    checkIfExists.NormalizedEmail = userToEdit.EmailAddress.ToLower();
                    checkIfExists.PhoneNumber = userToEdit.PhoneNumber;
                    checkIfExists.EmailConfirmed = false;
                    checkIfExists.LockoutEnabled = false;

                    //Update the user data
                    _context.Users.Update(checkIfExists);
                    _context.UserRoles.Add(userRoles);
                }

            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Remove User by id
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(string Id)
        {
            User user = _context.Users.Find(Id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserVM> GetAll()
        {
            var users = _context.Users.Select(MapToEditUserVM()).ToList();

            return users;
        }


        /// <summary>
        /// Get all Roles
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EditRolesVM> GetAllRoles()
        {
            var roles = _context.Roles.Select(MapToEditRolesVM()).ToList();

            return roles;
        }


        /// <summary>
        /// Find User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User FindUser(string id)
        {
            var user = _context.Users.Find(id);

            return user;
        }

        /// <summary>
        /// Returns the user role of the given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IdentityRole FindUserRole(UserVM user)
        {
            var roleId = _context.UserRoles.FirstOrDefault(x => x.UserId == user.Id);
            if (roleId != null)
            {
                var role = _context.Roles.Find(roleId.RoleId);
                return role;

            }

            return null;
        }

        /// <summary>
        /// Returns the role by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IdentityRole FindUserRoleById(string Id)
        {
            var roleId = _context.UserRoles.FirstOrDefault(x => x.UserId == Id);
            if (roleId != null)
            {
                var role = _context.Roles.Find(roleId.RoleId);

                return role;
            }

            return new IdentityRole();
        }

        /// <summary>
        /// Mapping
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<IdentityRole, EditRolesVM>> MapToEditRolesVM()
        {
            return x => new EditRolesVM()
            {
                Id = x.Id,
                Name = x.Name
            };
        }

        /// <summary>
        /// Mapping
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<User, UserVM>> MapToEditUserVM()
        {
            return x => new UserVM()
            {
                Id = x.Id,
                Username = x.UserName,
                EmailAddress = x.Email,
            };
        }

    }
}
