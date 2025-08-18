using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Backend.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
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
        private readonly IEmailSender emailSender;
        public UserService(UserManager<AppUser> userManager, IConfiguration configuration, IEmailSender emailSender)
        {
            this.configuration = configuration;
            this.emailSender = emailSender;
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
            var result = await UserManager.CreateAsync(user, request.Password);
            var roleResult = await UserManager.AddToRoleAsync(user, "User");
            if (result.Succeeded)
            {
                var VerifyToken = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                var goodToken = Uri.EscapeDataString(VerifyToken);
                var url = $"http://localhost:5173/verify?userId={user.Id}&token={goodToken}";
                await emailSender.SendEmailAsync(user.Email, "Test", $"<h2>Hello This Is Test Email</h2> <a href='{url}'>Confirm </a>");
                return new RegisterResponse()
                {
                    Id = user.Id
                };
            }
            else
            {
                return null;
            }

        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await UserManager.FindByEmailAsync(request.Email);
            if (user == null) {
                return new LoginResponse() {
                    Token = "email" };
            }
            var verify = await UserManager.IsEmailConfirmedAsync(user);
            if (!verify)
            {
                throw new Exception("Email Not Verified");
            }
            var result = await UserManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return new LoginResponse()
                {
                    Token = "password"
                };
            }
            var token = await CreateTokenAsync(user);
            return new LoginResponse() { Token = token };
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

            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secritKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt")["SecretKey"]));
            var cridentials = new SigningCredentials(secritKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: Claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: cridentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        public async Task<string> VerifiyEmail(string userId, string token)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("User Not Found");
            }
            var res = await UserManager.ConfirmEmailAsync(user, token);
            if (res.Succeeded)
            {
                return "Email Verified Successfully";
            }
            else
            {
                throw new Exception("Email Verification Failed");
            }
        }

        public async Task<ICollection<UserResponse>> getAllWithAddress()
        {
                var users= await UserManager.Users.Include(e=> e.Address).ToListAsync();
            var finalUsers= new List<UserResponse>();
            foreach (var u in users)
            {
                var Roles =await UserManager.GetRolesAsync(u);
                finalUsers.Add(new UserResponse()
                {
                    Id = u.Id,
                    UserName=u.UserName,
                    Email=u.Email,
                    PhoneNumber=u.PhoneNumber,
                    Address=u.Address.Adapt<AddressFullResponse>(),
                    Roles = Roles,
                    Status=u.EmailConfirmed,
                });
            }
            return finalUsers;
        }
    }
}
