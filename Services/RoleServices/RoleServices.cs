using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebProjectExam.Database;
using WebProjectExam.Models.ViewModels.RoleVMs;
using WebProjectExam.Models.ViewModels.UserVMs;

namespace WebProjectExam.Services.RoleServices
{
    public class RoleServices : IRoleServices
    {
        private readonly ShoeStoreDbContext _context;

        public RoleServices(ShoeStoreDbContext context)
        {
            _context = context;
        }
        public void Edit(RoleVm role)
        {
            var RoleToEdit = _context.Roles.FirstOrDefault(x => x.Id == role.Id);
            if (RoleToEdit != null)
            {
                RoleToEdit.Name = role.Name;
                _context.Roles.Update(RoleToEdit);
            }
            else
            {
                IdentityRole newRole = new IdentityRole()
                {
                    Name = role.Name,
                    NormalizedName = role.Name.ToLower()
                };
                _context.Roles.Add(newRole);
            }
            _context.SaveChanges();
        }
        public void Delete(string Id)
        {
            var roleToDelete = _context.Roles.FirstOrDefault(x => x.Id == Id);
            if (roleToDelete != null)
            {
                _context.Roles.Remove(roleToDelete);
            }
            _context.SaveChanges();
        }
        public IEnumerable<RoleVm> GetAllRoles()
        {
            var allRoles = _context.Roles.Select(MapToRolesVM()).ToList();

            return allRoles;
        }

        public IdentityRole GetRoleById(string Id)
        {
            var role = _context.Roles.Find(Id);

            if (role != null)
            {
                return role;
            }

            return null;
        }

        private static Expression<Func<IdentityRole, RoleVm>> MapToRolesVM()
        {
            return x => new RoleVm()
            {
                Id = x.Id,
                Name = x.Name
            };
        }
    }
}
