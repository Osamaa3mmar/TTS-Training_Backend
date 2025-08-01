using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Backend.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> UserManager;
        public UserService(UserManager<AppUser> userManager)
        {
            this.UserManager = userManager;
        }
        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var user = new AppUser()
            {
                Email = request.Email,
                UserName = request.Email,
                Address = new Address()
            };
            var result=await UserManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new RegisterResponse()
                {
                   Id=user.Id
                };
            }
            else
            {
                return null;
            }

        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user =await UserManager.FindByEmailAsync(request.Email);
            if (user == null) {
                return null;
            }
            var result = await UserManager.CheckPasswordAsync(user,request.Password);
            if (!result)
            {
                return null;
            }
            

            return new LoginResponse() { Token="Test"};
        }
    }
}
