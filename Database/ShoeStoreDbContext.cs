using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebProjectExam.Models.Entities;
using Microsoft.AspNetCore.Identity;
using WebProjectExam.Models.ViewModels;

namespace WebProjectExam.Database
{
    public class ShoeStoreDbContext : IdentityDbContext<User, IdentityRole, string>
    {

        public IConfiguration Configuration { get; }

        public ShoeStoreDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<ShoeToTag> ShoeToTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration["connectionString:DefaultConnection"]);
                                      
        }
    }
}
