
using Backend.BLL.Services.Classes;
using Backend.BLL.Services.Interfaces;
using Backend.DAL.Data;
using Backend.DAL.Models;
using Backend.DAL.Repository.Classes;
using Backend.DAL.Repository.Interfaces;
using Backend.DAL.Utils;
using Backend.PL.Utils;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

namespace Backend.PL
{
    public class Program
    {
        public  static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            builder.Services.AddHttpContextAccessor();

            //Dep injection
            builder.Services.AddDbContext<ApplicationDbContext>(op=>op.UseSqlServer(builder.Configuration.GetConnectionString("MyConn")));

            builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            //repositories
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentsRepository>();

            //services
            builder.Services.AddScoped<ICategoryService,CategoryService>(); 
            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<IAddressService,AddressService>();
            builder.Services.AddScoped<IProductService,ProductService>();
            builder.Services.AddScoped<IOrderService,OrderService>();
            builder.Services.AddScoped<IPaymentsServices,PaymentServices>();
            builder.Services.AddScoped<ISeedData,SeedData>();
            builder.Services.AddScoped<FilesUtiles>();
            builder.Services.AddScoped<IEmailSender,EmailSetting>();

            //Utils
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
            var cloudinarySettings = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            var account = new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret);
            var cloudinary = new Cloudinary(account);

            builder.Services.AddSingleton(cloudinary);



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt")["SecretKey"]))
            };
        });



            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            var scope =app.Services.CreateScope();
            var objOfSeedData=scope.ServiceProvider.GetRequiredService<ISeedData>();
            await objOfSeedData.DataSeedingAsync();
            await objOfSeedData.IdentityDataSeeding();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
