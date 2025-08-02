
using Backend.BLL.Services.Classes;
using Backend.BLL.Services.Interfaces;
using Backend.DAL.Data;
using Backend.DAL.Models;
using Backend.DAL.Repository.Classes;
using Backend.DAL.Repository.Interfaces;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Backend.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();




            //Dep injection
            builder.Services.AddDbContext<ApplicationDbContext>(op=>op.UseSqlServer(builder.Configuration.GetConnectionString("MyConn")));

            builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            //repositories
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            //services
            builder.Services.AddScoped<ICategoryService,CategoryService>(); 
            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<IAddressService,AddressService>();
            builder.Services.AddScoped<IProductService,ProductService>();
            builder.Services.AddScoped<IOrderService,OrderService>();
            builder.Services.AddScoped<FilesUtiles>();


            //Utils
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
            var cloudinarySettings = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            var account = new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret);
            var cloudinary = new Cloudinary(account);

            builder.Services.AddSingleton(cloudinary);
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
