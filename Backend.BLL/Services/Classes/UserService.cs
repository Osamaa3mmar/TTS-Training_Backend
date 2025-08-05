using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Backend.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> UserManager;
        private readonly IConfiguration configuration;
        public UserService(UserManager<AppUser> userManager,IConfiguration configuration)
        {
            this.configuration = configuration;
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
            var roleResult =await UserManager.AddToRoleAsync(user, "User"); 
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
                return new LoginResponse() {
                Token="email"};
            }
            var result = await UserManager.CheckPasswordAsync(user,request.Password);
            if (!result)
            {
                return new LoginResponse()
                {
                    Token = "password"
                };
            }
            var token =await CreateTokenAsync(user);
            return new LoginResponse() { Token=token};
        }
    
    
        private async Task<string> CreateTokenAsync(AppUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim("Email",user.Email),
                new Claim("Id",user.Id),
                new Claim("UserName",user.UserName)

            };


            var Roles = await UserManager.GetRolesAsync(user);

            foreach(var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secritKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt")["SecretKey"]));
            var cridentials = new SigningCredentials(secritKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims:Claims,
                expires:DateTime.Now.AddDays(2),
                signingCredentials:cridentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
