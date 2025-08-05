using Backend.DAL.Data;
using Backend.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        public SeedData(ApplicationDbContext context,RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }
        public async Task DataSeedingAsync()
        {
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }


            if (!(await context.Categories.AnyAsync()))
            {
                await context.Categories.AddRangeAsync(
                    new Category {
                        Name= "Technology",
                        Description= "Shop the latest gadgets, electronics, and smart devices for work or play."
                    },
                    new Category
                    {
                        Name = "Health & Fitness",
                        Description = "Find gear, supplements, and tools to support your active and healthy lifestyle."
                    }, new Category
                    {
                        Name = "Travel",
                        Description = "Everything you need for your next trip — from luggage to travel-friendly accessories."
                    }, new Category
                    {
                        Name = "Education",
                        Description = "Explore books, courses, and learning tools to grow your skills and knowledge."
                    }, new Category
                    {
                        Name = "Entertainment",
                        Description = "Browse fun products like board games, movies, collectibles, and more."
                    }
                    );
            }

            await context.SaveChangesAsync();

        }

        public async Task IdentityDataSeeding()
        {
             if(!await context.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("User"));

            }

             if(!await context.Users.AnyAsync())
            {
                var user1 = new AppUser()
                {
                    UserName = "Osama",
                    Email = "osama1111222@gmail.com",
                    Address=new Address
                    {
                        City = "Nablus",
                        Country = "Palstine",
                        Street = "al-jobil",
                        PostalCode="34534"
                    }
                };
                var user2 = new AppUser()
                {
                    UserName = "Ahmed",
                    Email = "ahmed@gmail.com",
                    Address= new Address
                    {
                        City = "Cairo",
                        Country = "Egypt",
                        Street = "123 Main St",
                        PostalCode = "12345"
                    }
                };

                await userManager.CreateAsync(user1, "Osama1234@");
                await userManager.CreateAsync(user2, "Ahmed1234@");
                
                await userManager.AddToRoleAsync(user1, "Admin");
                await userManager.AddToRoleAsync(user2, "User");

                await context.SaveChangesAsync();
            }
        }
    }
}
