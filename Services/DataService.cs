using FlynnNotesBlog.Data;
using FlynnNotesBlog.Enums;
using FlynnNotesBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlynnNotesBlog.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;
        public DataService(ApplicationDbContext dbContext, 
                           RoleManager<IdentityRole> roleManager, 
                           UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync() 
        {
            //Task 1: Create DataBase from Migrations
            await _dbContext.Database.MigrateAsync();

            // Task 2: Seed a Roles into the system.
            await SeedRolesAsync();

            // Task 3: Seed a few users into the system.
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            // If there are Roles in the system, do nothing.
            if (_dbContext.Roles.Any()) 
            {
                return;
            }

            // Otherwise creat Roles.
            foreach(var role in Enum.GetNames(typeof(BlogRole)))
            {
                //Use Role Manger to create Roles.
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            // If there are Users in the system, do nothing.
            if (_dbContext.Users.Any())
            {
                return;
            }

            // Step 1: Creates new instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "jflynn@mailinator.com",
                UserName = "jflynn@mailinator.com",
                FirstName = "John",
                LastName = "Flynn",
                DisplayName = "Fullsailer",
                PhoneNumber = "(800) 555-1212",
                EmailConfirmed = true,
            };

            // Step 2: Create new user with UserManager that is defined by adminUser.
            await _userManager.CreateAsync(adminUser, "Abc&123!");

            // Step 3: Add new user to the Administrator role.
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());


            //Create Moderator User
            //Step 1: Creat new instance of BlogUser
            var modUser = new BlogUser()
            {
                Email = "Rflynn@mailinator.com",
                UserName = "Rflynn@mailinator.com",
                FirstName = "Remington",
                LastName = "Flynn",
                DisplayName = "TheCutestDemon",
                PhoneNumber = "(800) 555-1213",
                EmailConfirmed = true,
            };

            // Step 2: Create new user with UserManager that is defined by modUser.
            await _userManager.CreateAsync(modUser, "Abc&123!");

            // Step 3: Add new user to the Moderator role.
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());

        }
    }
}
